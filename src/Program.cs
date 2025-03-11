using System;
using System.IO;
using System.Text;

/*  The program does the following:
    - takes in an input directory and output directory
    - takes in the path to a template file
    - browses the input directory recursively, and converts .md files into blog entries
        - we can write a function that opens a file, converts into
*/

// Look at arguments
// We want to make sure the program has an input/output directory & a template file
ArgumentParser arg = new ArgumentParser(args);
if (!arg.Ready())
{
    Console.WriteLine(arg.GetNotice());
    return;
}

// Open template file & get all files in the input directory
StreamReader templateFile = File.OpenText(arg.TemplatePath);
HashSet<string> files = Utility.GetFilesRecursively(arg.InputDir);

// Want to create a Post object for each file
// Most likely, we'll add each Post object to some dynamic array
// so that a sidebar can be made with the most recent posts, and folder navigations
foreach (string path in files)
{
    StreamReader postFile = File.OpenText(path);
    Post post = new Post(postFile, templateFile);
    string newPath = Utility.GetOutputPath(path, arg.InputDir, arg.OutputDir);

    post.WritePageTo(newPath);
}



