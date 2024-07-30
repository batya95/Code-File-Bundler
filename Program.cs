using myfib;
using System.CommandLine;
using System.Diagnostics;
using System.IO;
using System.Text;


var bundleOption = new Option<FileInfo>(new[] { "--output", "-o" }, "File path and name");

var languageOption = new Option<string>(new[] { "--language", "-l" },
    "An option that must be one or more the values of a static list.");
languageOption.IsRequired = true;

var noteOption = new Option<bool>(new[] { "--note", "-n" }, "is note");
var autherOption = new Option<string>(new[] { "--auther", "-a" }, "The name of creator the file");
var sortOption = new Option<bool>(new[] { "--sort", "-s" }, "How to sore the file");
var removeEmptyLinesOption = new Option<bool>(new[] { "--remove", "-r" }, "Delete the empty and white lines");
var bundleCommand = new Command("bundle", "Bundle code files to a single file");
var output2 = new Option<FileInfo>("--output", "file path and name");
var rsp = new Command("create-rsp");
rsp.AddOption(output2);

rsp.SetHandler((output2) =>
{
    string command = "bundle ", temp = "";
    Console.WriteLine("How do you want to call the bundle file");
    temp = Console.ReadLine();
    command += "--output " + temp + " ";

    Console.WriteLine("Do you want to add a note with the source of the file, before each file (yes/no)");
    temp = Console.ReadLine();
    if (temp == "yes")
        command += "--note ";

    Console.WriteLine("Do you want to order the files? (yes/no)");
    temp = Console.ReadLine();
    if (temp == "yes")
        command += "--sort ";

    Console.WriteLine("Do you want to delete empty lines? (yes/no)");
    temp = Console.ReadLine();
    if (temp == "yes")
        command += "--remove ";

    Console.WriteLine("Which language's files do you want c#/java/angular/react/sql/all");
    temp = Console.ReadLine();
    command += "--language " + temp + " ";

    File.AppendAllText(output2.FullName, command);

    Console.WriteLine("file created successfully");


}, output2);
bundleCommand.AddOption(bundleOption);
bundleCommand.AddOption(languageOption);
bundleCommand.AddOption(noteOption);
bundleCommand.AddOption(autherOption);
bundleCommand.AddOption(removeEmptyLinesOption);
bundleCommand.AddOption(sortOption);
static string[] buildArrLanguage(string path, string c)
{
    string[] arr = Directory.GetFiles(path, "*." + c, SearchOption.AllDirectories);
    return arr;

}

bundleCommand.SetHandler((output, language, note, auther, remove, sort) =>
{
    try
    {//מחזירה את הניתוב שבו נמצאים
        string currentPath = Environment.CurrentDirectory;
        string[] files;
        string c;
        language = language.ToLower();
        switch (language)
        {
            case "angular":
                c = "ts";
                string[] arrTs = buildArrLanguage(currentPath, c);
                c = "html";
                string[] arrHtml = buildArrLanguage(currentPath, c);
                c = "scss";
                string[] arrScss = buildArrLanguage(currentPath, c);
                files = arrTs.Concat(arrHtml).Concat(arrScss).ToArray();
                break;
            case "c":
                c = "c";
                files = buildArrLanguage(currentPath, c);
                break;
            case "c#":
            case "c sharp":
            case "csharp":
                c = "cs";
                files = buildArrLanguage(currentPath, c);
                break;

            case "c++":
                c = "cpp";
                files = buildArrLanguage(currentPath, c);
                break;

            case "java":
                c = "java";
                files = buildArrLanguage(currentPath, c);
                break;
            case "javascript":
            case "js":
            case "jscript":
            case "java script":
                c = "js";
                string[] arrJs = buildArrLanguage(currentPath, c); ;
                c = "html";
                string[] arrHtml1 = buildArrLanguage(currentPath, c); ;
                c = "css";
                string[] arrCss1 = buildArrLanguage(currentPath, c);
                files = arrJs.Concat(arrHtml1).Concat(arrCss1).ToArray();
                break;
            case "python":
                c = "py";
                files = buildArrLanguage(currentPath, c); ;
                break;
            case "react":
                c = "jsx";
                string[] arrJs2 = buildArrLanguage(currentPath, c); ;
                c = "html";
                string[] arrHtml2 = buildArrLanguage(currentPath, c); ;
                c = "css";
                string[] arrCss2 = buildArrLanguage(currentPath, c);
                files = arrJs2.Concat(arrHtml2).Concat(arrCss2).ToArray();
                break;
            case "sql":
                c = "sql";
                files = buildArrLanguage(currentPath, c);
                break;
            case "all":
                c = "js";
                string[] js = buildArrLanguage(currentPath, c); ;
                c = "html";
                string[] html = buildArrLanguage(currentPath, c); ;
                c = "css";
                string[] css = buildArrLanguage(currentPath, c);
                c = "py";
                string[] py = buildArrLanguage(currentPath, c);
                c = "sql";
                string[] sql = buildArrLanguage(currentPath, c);
                c = "ts";
                string[] ts = buildArrLanguage(currentPath, c);
                c = "jsx";
                string[] jsx = buildArrLanguage(currentPath, c);
                c = "cs";
                string[] cs = buildArrLanguage(currentPath, c);
                c = "c";
                string[] cArr = buildArrLanguage(currentPath, c);
                c = "cpp";
                string[] cpp = buildArrLanguage(currentPath, c);
                c = "java";
                string[] java = buildArrLanguage(currentPath, c);
                c = "scss";
                string[] scss = buildArrLanguage(currentPath, c);

                files = js.Concat(html).Concat(css).Concat(py).Concat(sql).Concat(ts).Concat(jsx).
                Concat(cs).Concat(cArr).Concat(cpp).Concat(java).Concat(scss).ToArray();
                break;
            default:
                Console.WriteLine($"The language:{language} is invalid,all the code files will be in the new file");
                files = Directory.GetFiles(currentPath, "*.*", SearchOption.AllDirectories);
                break;
        }
        if (sort)
        {
            files = files.OrderBy(p => Path.GetExtension(p)).ToArray();
        }
        else
        {
            files = files.OrderBy(p => Path.GetFileName(p)).ToArray();
        }
       
        if (auther != null)
        {
            auther = auther.ToLower();

            File.WriteAllText(output.FullName, "The creator:" + auther);
        }

        files.ToList();
        foreach (string codeFile in files)
        {
            if (note)
            {
                File.AppendAllText(output.FullName, codeFile);
            }
            if (remove)
            {
                var lines = File.ReadAllLines(codeFile);
                var cleanLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
                File.AppendAllLines(output.FullName, cleanLines);

            }
            else
            {
                var code = File.ReadAllLines(codeFile);
                File.AppendAllLines(output.FullName, code);

            }
        }

        Console.WriteLine($"The file was created successfully in: {output.FullName}");
    }
    catch (Exception ex)
    { Console.WriteLine("Error: File path is invalid"); }
}, bundleOption, languageOption, noteOption, autherOption, removeEmptyLinesOption, sortOption);
var rootCommand = new RootCommand("Root command for File Bundler CLI");

rootCommand.AddCommand(bundleCommand);
rootCommand.AddCommand(rsp);
rootCommand.InvokeAsync(args);

