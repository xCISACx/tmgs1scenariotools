using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class App
    {

        public int TotalFiles { get; private set; }
        public int ProcessedFiles { get; private set; }

        static Regex groupRegex = new Regex("[a-z]+");

        public delegate void IntChangeDelegate(int val);
        public delegate void FileChangeDelegate(int fileProcessed, string lastFile);

        public event IntChangeDelegate TotalFilesChanged;
        public event FileChangeDelegate ProcessedFilesChanged;

        public bool StopRequested { get; private set; } = false;

        public bool ForceCompilation = true;

        public App()
        {

        }

        public void Stop()
        {
            StopRequested = true;
        }
        class ExceptionHolder
        {
            public Exception exception;
        }
        
        public void CompileDirs(string sourceDir, string targetDir)
        {
            StopRequested = false;
            List<string> files = getScripts(sourceDir).ToList();
            TotalFiles = files.Count;
            TotalFilesChanged.Invoke(files.Count);
            ProcessedFiles = 0;

            var v = new ExceptionHolder() ;
            using (var countdownEvent = new CountdownEvent(files.Count))
            {
                foreach (String file in files)
                {
                    WaitCallback wc = new WaitCallback((obj) => {
                        var exceptionHolder = obj as ExceptionHolder;
                        
                        if (!StopRequested) {
                            try
                            {
                                processSingleFile(file, targetDir);
                            } catch (Exception e)
                            {
                                exceptionHolder.exception = new FileException("fail to compile", file, e);
                                //Debug.WriteLine("Fail to compile {0}", new string[] { file });
                                StopRequested = true;
                            }
                        }
                        countdownEvent.Signal();
                        ProcessedFiles++;
                        ProcessedFilesChanged.Invoke(ProcessedFiles, file);
                    });
                    ThreadPool.QueueUserWorkItem(wc, v);
                }
                countdownEvent.Wait();
            }
            if (v.exception != null)
            {
                throw v.exception;
            }
        }

        void processSingleFile(string sourceFile, string targetDir)
        {
            String scenarioName = Path.GetFileNameWithoutExtension(sourceFile);
           

            String scenarioGroup = groupRegex.Match(scenarioName).ToString();

            String outDir = Path.Combine(targetDir, scenarioGroup);
            Directory.CreateDirectory(outDir);
            String outFile = Path.Combine(targetDir, scenarioGroup, scenarioName + ".bytes");

            Compiler compiler = new Compiler(sourceFile, outFile);

            if (ForceCompilation || compiler.NeedCompilation())
            {
                compiler.Compile();
            }
            else
            {
                return;
            }


            //String referencePath = @"C:\Asset Dump\TextAsset\";
            //String existingFile = Path.Combine(referencePath, scenarioName + ".bytes");

            //compare(outFile, existingFile);
        }



        IEnumerable<String> getScripts(string sourceDir)
        {
            Matcher matcher = new Matcher();
            matcher.AddInclude("**/*.c");
            IEnumerable<String> paths = matcher.GetResultsInFullPath(sourceDir);

            return paths;

        }

        public void DecompileDirs(string sourceDir, string targetDir)
        {
            StopRequested = false;
            Matcher matcher = new Matcher();
            matcher.AddInclude("**/*.bytes");
            IEnumerable<String> paths = matcher.GetResultsInFullPath(sourceDir);
            List<String> listOfFiles = paths.ToList();

            TotalFilesChanged.Invoke(listOfFiles.Count);
            int fileProcessed = 0;
            foreach (String filePath in listOfFiles)
            {
                if (!StopRequested)
                {
                    Debug.WriteLine("decompiling {0}", new string[] { filePath });
                    exportScript(filePath, targetDir);
                }
                fileProcessed++;
                ProcessedFilesChanged.Invoke(fileProcessed, filePath);
            }

            return;

        }

        void exportScript(string filePath, string targetDir)
        {
            String nameWithoutExt = Path.GetFileNameWithoutExtension(filePath);

            String group = groupRegex.Match(nameWithoutExt).ToString();

            String outDir = Path.Combine(targetDir, group);
            Directory.CreateDirectory(outDir);

            String outPath = Path.Combine(targetDir, group, nameWithoutExt + ".c");

            Reader reader = new Reader(filePath, nameWithoutExt);
            ScriptWriter writer = new ScriptWriter(outPath);

            writer.write(reader.ScriptTokens());
            //Debug.WriteLine(nameWithoutExt);
        }

        static void compare(string ourFile, string referenceFile)
        {
            FileStream ourStream = File.OpenRead(ourFile);
            FileStream referenceStream = File.OpenRead(referenceFile);

            long length = ourStream.Length;
            if (referenceStream.Length != length)
            {

                //throw new Exception("length not equal");
            }

            for (int i = 0; i < length / 0x10; i++)
            {
                byte[] bytesA = new byte[0x10];
                byte[] bytesB = new byte[0x10];
                ourStream.Read(bytesA, 0, 0x10);
                referenceStream.Read(bytesB, 0, 0x10);

                if (!bytesA.SequenceEqual(bytesB))
                {
                    Debug.WriteLine(referenceFile);
                    Debug.WriteLine(ourFile);
                    long offset = length * 0x10;
                    throw new Exception(ourFile + " is not equal");
                }
            }
        }
    }
}
