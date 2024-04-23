//Written for Helbreath Nemesis. https://steamcommunity.com/app/2857560/
using System.IO;

namespace Helbreath_Nemesis_Unpacker
{
    class Program
    {
        static BinaryReader br;
        static void Main(string[] args)
        {
            br = new BinaryReader(File.OpenRead(args[0]));
            if (new string(br.ReadChars(17)) != "<Pak file header>")
                throw new System.Exception("Please input a PAK file from Helbreath Nemesis.");

            br.ReadBytes(3);

            int count = br.ReadInt32();
            System.Collections.Generic.List<Subfile> subfiles = new();
            for (int i = 0; i < count; i++)
                subfiles.Add(new());

            string path = Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]);
            Directory.CreateDirectory(path);

            int n = 0;
            foreach (Subfile file in subfiles)
            {
                br.BaseStream.Position = file.start;
                BinaryWriter bw = new(File.Create(path + "//" + n + ".Sprite"));
                bw.Write(br.ReadBytes(file.size));
                n++;
            }
        }

        class Subfile
        {
            public int start = br.ReadInt32();
            public int size = br.ReadInt32();
        }
    }
}
