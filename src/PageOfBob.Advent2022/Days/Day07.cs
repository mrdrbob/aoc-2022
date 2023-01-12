using PageOfBob.Parsing.Compiled.GeneralRules;
using static PageOfBob.Parsing.Compiled.StringRules.Rules;
using static PageOfBob.Parsing.Compiled.GeneralRules.Rules;
using PageOfBob.Parsing.Compiled;
using System.Linq;
using System.Xml.Schema;

namespace PageOfBob.Advent2022.Days
{
    public static class Day07
    {

        static readonly Lazy<IParser<List<ICommand>>> CommandParser = new Lazy<IParser<List<ICommand>>>(BuildParser);

        static IParser<List<ICommand>> BuildParser()
        {
            var ignoreNewlines = Match('\r', '\n').Many(false);

            // $ cd ..
            var changeDownCommand = Text("$ cd ..")
                .ThenIgnore(ignoreNewlines)
                .Map(s => (ICommand)new ChangeDownCommand());

            // $ cd ..
            var changeRootCommand = Text("$ cd /")
                .ThenIgnore(ignoreNewlines)
                .Map(s => (ICommand)new ChangeRootCommand());

            // $ cd fdsjfds
            var changeUpCommand = Text("$ cd ")
                .ThenKeep(Match('\n', '\r').NotText().Required())
                .ThenIgnore(ignoreNewlines)
                .Map(s => (ICommand)new ChangeUpCommand(s));


            // 14848514 b.txt
            var fileListingResult = IsDigitText
                .ThenIgnore(Match(' '))
                .Then(Match('\n', '\r').NotText().Required(), (size, name) => (IListCommandLine)new ListCommandFile(ulong.Parse(size), name))
                .ThenIgnore(ignoreNewlines);

            // dir d
            var directoryListing = Text("dir ")
                .ThenKeep(Match('\n', '\r').NotText().Required())
                .ThenIgnore(ignoreNewlines)
                .Map(x => (IListCommandLine)new ListCommandDirectory(x));

            var listResult = Any(fileListingResult, directoryListing)
                .ThenIgnore(ignoreNewlines)
                .Many();

            // $ ls
            var listCommand = Text("$ ls")
                .ThenIgnore(ignoreNewlines)
                .ThenKeep(listResult)
                .Map(x => (ICommand)new ListCommand(x.ToArray()));

            var parser = Any(changeRootCommand, changeDownCommand, changeUpCommand, listCommand)
                .Many()
                .CompileParser("CommandParser");

            return parser;
        }

        private static Node ParseToTree(string input)
        {
            var parser = CommandParser.Value;

            if (!parser.TryParse(input, out var parsedCommands, out int eof))
            {
                throw new FormatException("Bad input!");
            }

            var rootNode = new Node("/", true, 0, default);
            var pwd = rootNode;

            foreach (var command in parsedCommands)
            {
                switch (command)
                {
                    case ChangeRootCommand cdRoot:
                        pwd = rootNode;
                        break;
                    case ChangeDownCommand cdDownCommand:
                        if (pwd.ParentNode is null)
                            throw new InvalidOperationException("Could not cd .. from root");
                        pwd = pwd.ParentNode;
                        break;
                    case ChangeUpCommand cdUpCommand:
                        var existing = pwd.ChildNodes.SingleOrDefault(x => x.Name == cdUpCommand.Destination);
                        if (existing is null)
                        {
                            var newNode = new Node(cdUpCommand.Destination, true, 0, pwd);
                            pwd = newNode;
                        }
                        else if (!existing.IsDirectory)
                        {
                            throw new InvalidOperationException("Could not CD into a file");
                        }
                        else
                        {
                            pwd = existing;
                        }
                        break;
                    case ListCommand list:
                        foreach (var item in list.Lines)
                        {
                            switch (item)
                            {
                                case ListCommandFile file:
                                    var existingFile = pwd.ChildNodes.SingleOrDefault(x => x.Name == file.Name);
                                    if (existingFile is not null)
                                    {
                                        if (existingFile.Size != file.Size)
                                            throw new InvalidOperationException("File size mismatch");
                                    }
                                    else
                                    {
                                        pwd.ChildNodes.Add(new Node(file.Name, false, file.Size, pwd));
                                    }
                                    break;
                                case ListCommandDirectory dir:
                                    var existingDir = pwd.ChildNodes.SingleOrDefault(x => x.Name == dir.Name);
                                    if (existingDir is null)
                                    {
                                        pwd.ChildNodes.Add(new Node(dir.Name, true, 0, pwd));
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }

            return rootNode;
        }

        public static void PartOne(string input)
        {
            var rootNode = ParseToTree(input);
            var totalSize = rootNode.ListDirectories()
                .Where(n => n.TotalSize < 100000)
                .Select(n => n.TotalSize)
                .Aggregate(0UL, (acc, v) => acc + v);


            Console.WriteLine($"Total size: {totalSize}");
        }

        public static void PartTwo(string input)
        {

            var rootNode = ParseToTree(input);

            const ulong totalDiskSize = 70000000;
            const ulong totalNecessarySpace = 30000000;
            ulong totalUsedSpace = rootNode.RecursiveSize();
            ulong totalFreeSpace = totalDiskSize - totalUsedSpace;
            ulong spaceNeeded = totalNecessarySpace - totalFreeSpace;

            var smallestLargeEnoughNode = rootNode.ListDirectories()
                .ToList()
                .OrderBy(x => x.TotalSize)
                .Where(x => x.TotalSize >= spaceNeeded)
                .First();

            Console.WriteLine($"Size: {smallestLargeEnoughNode.TotalSize}");
        }

        public interface ICommand { }
        public record ChangeDownCommand() : ICommand { }
        public record ChangeRootCommand() : ICommand { }
        public record ChangeUpCommand(string Destination) : ICommand;

        public record ListCommand(IListCommandLine[] Lines) : ICommand;

        public interface IListCommandLine { }
        public record ListCommandFile(ulong Size, string Name) : IListCommandLine;
        public record ListCommandDirectory(string Name) : IListCommandLine;

        public record DirectoryInfo(string Name, ulong TotalSize);

        public record Node(string Name, bool IsDirectory, ulong Size, Node? ParentNode)
        {
            public IList<Node> ChildNodes { get; } = new List<Node>();

            public ulong RecursiveSize()
            {
                if (!IsDirectory)
                {
                    return Size;
                }

                return ChildNodes.Aggregate(0UL, (acc, node) => acc + node.RecursiveSize());
            }

            public IEnumerable<DirectoryInfo> ListDirectories()
            {
                if (!IsDirectory)
                    throw new InvalidOperationException("Cannot list directories on a file");

                foreach (var child in ChildNodes.Where(x => x.IsDirectory))
                {
                    ulong size = child.RecursiveSize();
                    yield return new DirectoryInfo(child.Name, size);

                    foreach (var subchild in child.ListDirectories())
                    {
                        yield return subchild;
                    }
                }
            }
        }
    }

}
