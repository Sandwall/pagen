using System.Text;

public class ArgumentParser
{
    public string InputDir = null;
    public string OutputDir = null;
    public string TemplatePath = null;

    private StringBuilder noticeBuilder = new StringBuilder();

    // Parse args in the constructor
    // Creator should only use input/output/template if Ready() returns true
    public ArgumentParser(string[] args)
    {
        if (args.Contains("--help") || args.Contains("-h"))
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            noticeBuilder.AppendLine($"pagen utility: version {version}");
            noticeBuilder.AppendLine("command format: dotnet run <inputDir> <outputDir> <templatePath>");

            return;
        }

        InputDir = args[0];
        OutputDir = args[1];
        TemplatePath = args[2];
    }

    public string GetNotice()
    {
        return noticeBuilder.ToString();
    }

    // This function determines if the program is ready to run
    // We require that there be an input/output dir, along with a template file
    public bool Ready()
    {
        if (InputDir == null || OutputDir == null || TemplatePath == null)
        {
            noticeBuilder.AppendLine("Please provide an input directory, output directory, and template path.");
            return false;
        }

        if (!Utility.IsDirectory(InputDir))
        {
            noticeBuilder.AppendLine("Please ensure input is not a directory.");
            return false;
        }

        if (!Utility.IsDirectory(OutputDir))
        {
            noticeBuilder.AppendLine("Please ensure output is not a directory.");
            return false;
        }

        if (!File.Exists(TemplatePath))
        {
            noticeBuilder.AppendLine("Please ensure template file exists.");
            return false;
        }

        return true;
    }
}