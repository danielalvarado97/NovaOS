using System;
using System.IO;
using Sys = Cosmos.System;

namespace NovaOS
{
    public class Kernel : Sys.Kernel
    {
        private Sys.FileSystem.CosmosVFS fs;
        private string currentDirectory = @"0:\";

        private string[] commandHistory = new string[5];
        private int historyCount = 0;

        protected override void BeforeRun()
        {
            Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.ESStandardLayout());

            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            Console.Clear();
            PlayStartSound();

            Console.WriteLine("================================");
            Console.WriteLine("         Benvingut a NovaOS     ");
            Console.WriteLine("================================");
            Console.WriteLine("Sistema de fitxers inicialitzat.");
            Console.WriteLine("Teclat configurat: ES Standard");
            Console.WriteLine("Escriu 'guide' per veure les comandes.");
            Console.WriteLine();
        }

        protected override void Run()
        {
            while (true)
            {
                Console.Write("NovaOS " + currentDirectory + "> ");
                string input = Console.ReadLine();

                if (input == null || input == "")
                {
                    continue;
                }

                ExecuteCommand(input, true);
            }
        }

        private void ExecuteCommand(string input, bool saveHistory)
        {
            if (saveHistory)
            {
                AddToHistory(input);
            }

            string[] parts = input.Split(' ');
            string command = parts[0];

            bool success = true;

            switch (command)
            {
                case "guide":
                    ShowHelp();
                    break;

                case "origin":
                    ShowOrigin();
                    break;

                case "wipe":
                    Console.Clear();
                    break;

                case "say":
                    Say(input);
                    break;

                case "shutdown":
                    Console.WriteLine("Apagant NovaOS...");
                    Cosmos.System.Power.Shutdown();
                    break;

                case "reborn":
                    Console.WriteLine("Reiniciant NovaOS...");
                    Cosmos.System.Power.Reboot();
                    break;

                case "add":
                    success = Calculate(parts, "+");
                    break;

                case "sub":
                    success = Calculate(parts, "-");
                    break;

                case "mul":
                    success = Calculate(parts, "*");
                    break;

                case "div":
                    success = Calculate(parts, "/");
                    break;

                case "mod":
                    success = Calculate(parts, "%");
                    break;

                case "sqrt":
                    success = SquareRoot(parts);
                    break;

                case "peek":
                    success = Peek(parts);
                    break;

                case "jump":
                    success = Jump(parts);
                    break;

                case "forge":
                    success = Forge(parts);
                    break;

                case "zap":
                    success = Zap(parts);
                    break;

                case "read":
                    success = ReadFile(parts);
                    break;

                case "write":
                    success = WriteFile(parts);
                    break;

                case "history":
                    ShowHistory();
                    break;

                case "again":
                    success = RunFromHistory(parts);
                    break;

                default:
                    Console.WriteLine("Comanda no reconeguda. Escriu 'guide'.");
                    success = false;
                    break;
            }

            if (success)
            {
                PlayOkSound();
            }
            else
            {
                PlayErrorSound();
            }

            Console.WriteLine();
        }

        private void PlayStartSound()
        {
            Console.Beep(700, 120);
            Console.Beep(900, 120);
            Console.Beep(1100, 160);
        }

        private void PlayOkSound()
        {
            Console.Beep(1000, 80);
        }

        private void PlayErrorSound()
        {
            Console.Beep(300, 180);
            Console.Beep(250, 180);
        }

        private void AddToHistory(string command)
        {
            if (historyCount < 5)
            {
                commandHistory[historyCount] = command;
                historyCount++;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    commandHistory[i] = commandHistory[i + 1];
                }

                commandHistory[4] = command;
            }
        }

        private void ShowHistory()
        {
            Console.WriteLine("Ultimes comandes:");

            for (int i = 0; i < historyCount; i++)
            {
                Console.WriteLine((i + 1) + " - " + commandHistory[i]);
            }
        }

        private bool RunFromHistory(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Us: again [numero]");
                return false;
            }

            int index;
            bool valid = int.TryParse(parts[1], out index);

            if (!valid || index < 1 || index > historyCount)
            {
                Console.WriteLine("Error: numero d'historial invalid.");
                return false;
            }

            string command = commandHistory[index - 1];
            Console.WriteLine("Executant: " + command);
            ExecuteCommand(command, false);

            return true;
        }

        private void ShowHelp()
        {
            Console.WriteLine("Comandes disponibles:");
            Console.WriteLine("----------------------");
            Console.WriteLine("guide                 - Mostra ajuda");
            Console.WriteLine("origin                - Informacio del sistema");
            Console.WriteLine("wipe                  - Neteja la pantalla");
            Console.WriteLine("say [text]            - Mostra un text");
            Console.WriteLine("shutdown              - Apaga el sistema");
            Console.WriteLine("reborn                - Reinicia el sistema");
            Console.WriteLine("add [a] [b]           - Suma dos nombres");
            Console.WriteLine("sub [a] [b]           - Resta dos nombres");
            Console.WriteLine("mul [a] [b]           - Multiplica dos nombres");
            Console.WriteLine("div [a] [b]           - Divideix dos nombres");
            Console.WriteLine("mod [a] [b]           - Calcula el modul");
            Console.WriteLine("sqrt [a]              - Calcula l'arrel quadrada");
            Console.WriteLine("peek                  - Mostra fitxers i carpetes");
            Console.WriteLine("jump [dir]            - Canvia de directori");
            Console.WriteLine("forge [dir]           - Crea un directori");
            Console.WriteLine("zap [dir]             - Elimina un directori buit");
            Console.WriteLine("read [fitxer]         - Mostra el contingut d'un fitxer");
            Console.WriteLine("write [fitxer] [text] - Escriu text en un fitxer");
            Console.WriteLine("history               - Mostra les ultimes cinc comandes");
            Console.WriteLine("again [numero]        - Torna a executar una comanda de l'historial");
        }

        private void ShowOrigin()
        {
            Console.WriteLine("NovaOS v0.1");
            Console.WriteLine("Sistema operatiu basat en CosmOS");
        }

        private void Say(string input)
        {
            if (input.Length <= 4)
            {
                Console.WriteLine("Us: say [text]");
                return;
            }

            Console.WriteLine(input.Substring(4));
        }

        private bool Calculate(string[] parts, string operation)
        {
            if (parts.Length < 3)
            {
                Console.WriteLine("Us: add/sub/mul/div/mod [num1] [num2]");
                return false;
            }

            double a;
            double b;

            bool validA = double.TryParse(parts[1], out a);
            bool validB = double.TryParse(parts[2], out b);

            if (!validA || !validB)
            {
                Console.WriteLine("Error: introdueix nombres valids.");
                return false;
            }

            double result = 0;

            if (operation == "+") result = a + b;
            else if (operation == "-") result = a - b;
            else if (operation == "*") result = a * b;
            else if (operation == "/")
            {
                if (b == 0)
                {
                    Console.WriteLine("Error: no es pot dividir entre zero.");
                    return false;
                }

                result = a / b;
            }
            else if (operation == "%")
            {
                if (b == 0)
                {
                    Console.WriteLine("Error: no es pot fer modul entre zero.");
                    return false;
                }

                result = a % b;
            }

            Console.WriteLine("Resultat: " + result);
            return true;
        }

        private bool SquareRoot(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Us: sqrt [num]");
                return false;
            }

            double number;
            bool valid = double.TryParse(parts[1], out number);

            if (!valid || number < 0)
            {
                Console.WriteLine("Error: nombre invalid.");
                return false;
            }

            Console.WriteLine("Resultat: " + Math.Sqrt(number));
            return true;
        }

        private string GetFullPath(string path)
        {
            if (path.Contains(@":\"))
            {
                return path;
            }

            return currentDirectory + path;
        }

        private bool Peek(string[] parts)
        {
            string path = currentDirectory;

            if (parts.Length >= 2)
            {
                path = GetFullPath(parts[1]);
            }

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Error: el directori no existeix.");
                return false;
            }

            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (string dir in directories)
            {
                Console.WriteLine("[DIR]  " + dir);
            }

            foreach (string file in files)
            {
                Console.WriteLine("[FILE] " + file);
            }

            return true;
        }

        private bool Jump(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Us: jump [directori]");
                return false;
            }

            string path = GetFullPath(parts[1]);

            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Error: el directori no existeix.");
                return false;
            }

            currentDirectory = path;
            return true;
        }

        private bool Forge(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Us: forge [directori]");
                return false;
            }

            string path = GetFullPath(parts[1]);

            if (Directory.Exists(path))
            {
                Console.WriteLine("Error: el directori ja existeix.");
                return false;
            }

            Directory.CreateDirectory(path);
            Console.WriteLine("Directori creat: " + path);
            return true;
        }

        private bool Zap(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Us: zap [directori]");
                return false;
            }

            string path = GetFullPath(parts[1]);

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Error: el directori no existeix.");
                return false;
            }

            Directory.Delete(path);
            Console.WriteLine("Directori eliminat: " + path);
            return true;
        }

        private bool ReadFile(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Us: read [fitxer]");
                return false;
            }

            string path = GetFullPath(parts[1]);

            if (!File.Exists(path))
            {
                Console.WriteLine("Error: el fitxer no existeix.");
                return false;
            }

            Console.WriteLine(File.ReadAllText(path));
            return true;
        }

        private bool WriteFile(string[] parts)
        {
            if (parts.Length < 3)
            {
                Console.WriteLine("Us: write [fitxer] [text]");
                return false;
            }

            string path = GetFullPath(parts[1]);
            string text = "";

            for (int i = 2; i < parts.Length; i++)
            {
                text += parts[i];

                if (i < parts.Length - 1)
                {
                    text += " ";
                }
            }

            File.WriteAllText(path, text);
            Console.WriteLine("Fitxer escrit: " + path);
            return true;
        }
    }
}