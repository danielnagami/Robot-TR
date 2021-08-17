using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeAnalyzerLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //var files = new List<string>();
            //GetCodeFiles("C:\\Studies\\desenvolvedor.io\\NerdStoreEnterprise", ref files);

            //int fileCounter = 1;
            //foreach (var file in files)
            //{
            //    Console.WriteLine($"File number {fileCounter} | Directory: {file}");
            //    Console.WriteLine("-----------------------------------------------");
            //    ReadFile(file);
            //    Console.WriteLine("-----------------------------------------------");
            //    fileCounter++;
            //}

            var decoded = Decode("77u/dXNpbmcgTWljcm9zb2Z0LkFzcE5ldENvcmUuQXV0aG9yaXphdGlvbjsK\ndXNpbmcgTWljcm9zb2Z0LkFzcE5ldENvcmUuTXZjOwp1c2luZyBOU0UuQkZG\nLkNvbXByYXMuTW9kZWxzOwp1c2luZyBOU0UuQkZGLkNvbXByYXMuU2Vydmlj\nZXM7CnVzaW5nIE5TRS5XZWJBUEkuQ29yZS5Db250cm9sbGVyczsKdXNpbmcg\nU3lzdGVtOwp1c2luZyBTeXN0ZW0uTGlucTsKdXNpbmcgU3lzdGVtLlRocmVh\nZGluZy5UYXNrczsKCm5hbWVzcGFjZSBOU0UuQkZGLkNvbXByYXMuQ29udHJv\nbGxlcnMKewogICAgW0F1dGhvcml6ZV0KICAgIHB1YmxpYyBjbGFzcyBDYXJy\naW5ob0NvbnRyb2xsZXIgOiBNYWluQ29udHJvbGxlcgogICAgewogICAgICAg\nIHByaXZhdGUgcmVhZG9ubHkgSUNhcnJpbmhvU2VydmljZSBfY2FycmluaG9T\nZXJ2aWNlOwogICAgICAgIHByaXZhdGUgcmVhZG9ubHkgSUNhdGFsb2dvU2Vy\ndmljZSBfY2F0YWxvZ29TZXJ2aWNlOwogICAgICAgIHByaXZhdGUgcmVhZG9u\nbHkgSVBlZGlkb1NlcnZpY2UgX3BlZGlkb1NlcnZpY2U7CgogICAgICAgIHB1\nYmxpYyBDYXJyaW5ob0NvbnRyb2xsZXIoSUNhcnJpbmhvU2VydmljZSBjYXJy\naW5ob1NlcnZpY2UsIElDYXRhbG9nb1NlcnZpY2UgY2F0YWxvZ29TZXJ2aWNl\nLCBJUGVkaWRvU2VydmljZSBwZWRpZG9TZXJ2aWNlKQogICAgICAgIHsKICAg\nICAgICAgICAgX2NhcnJpbmhvU2VydmljZSA9IGNhcnJpbmhvU2VydmljZTsK\nICAgICAgICAgICAgX2NhdGFsb2dvU2VydmljZSA9IGNhdGFsb2dvU2Vydmlj\nZTsKICAgICAgICAgICAgX3BlZGlkb1NlcnZpY2UgPSBwZWRpZG9TZXJ2aWNl\nOwogICAgICAgIH0KCiAgICAgICAgW0h0dHBHZXRdCiAgICAgICAgW1JvdXRl\nKCJjb21wcmFzL2NhcnJpbmhvIildCiAgICAgICAgcHVibGljIGFzeW5jIFRh\nc2s8SUFjdGlvblJlc3VsdD4gSW5kZXgoKQogICAgICAgIHsKICAgICAgICAg\nICAgcmV0dXJuIEN1c3RvbVJlc3BvbnNlKGF3YWl0IF9jYXJyaW5ob1NlcnZp\nY2UuT2J0ZXJDYXJyaW5obygpKTsKICAgICAgICB9CgogICAgICAgIFtIdHRw\nR2V0XQogICAgICAgIFtSb3V0ZSgiY29tcHJhcy9jYXJyaW5oby1xdWFudGlk\nYWRlIildCiAgICAgICAgcHVibGljIGFzeW5jIFRhc2s8aW50PiBPYnRlclF1\nYW50aWRhZGVDYXJyaW5obygpCiAgICAgICAgewogICAgICAgICAgICB2YXIg\ncXVhbnRpZGFkZSA9IGF3YWl0IF9jYXJyaW5ob1NlcnZpY2UuT2J0ZXJDYXJy\naW5obygpOwogICAgICAgICAgICByZXR1cm4gcXVhbnRpZGFkZT8uSXRlbnMu\nU3VtKGkgPT4gaS5RdWFudGlkYWRlKSA/PyAwOwogICAgICAgIH0KCiAgICAg\nICAgW0h0dHBQb3N0XQogICAgICAgIFtSb3V0ZSgiY29tcHJhcy9jYXJyaW5o\nby9pdGVtcyIpXQogICAgICAgIHB1YmxpYyBhc3luYyBUYXNrPElBY3Rpb25S\nZXN1bHQ+IEFkaWNpb25hckl0ZW1DYXJyaW5obyhJdGVtQ2FycmluaG9EVE8g\naXRlbVByb2R1dG8pCiAgICAgICAgewogICAgICAgICAgICB2YXIgcHJvZHV0\nbyA9IGF3YWl0IF9jYXRhbG9nb1NlcnZpY2UuT2J0ZXJQb3JJZChpdGVtUHJv\nZHV0by5Qcm9kdXRvSWQpOwoKICAgICAgICAgICAgYXdhaXQgVmFsaWRhckl0\nZW1DYXJyaW5obyhwcm9kdXRvLCBpdGVtUHJvZHV0by5RdWFudGlkYWRlKTsK\nICAgICAgICAgICAgaWYgKCFPcGVyYWNhb1ZhbGlkYSgpKSByZXR1cm4gQ3Vz\ndG9tUmVzcG9uc2UoKTsKCiAgICAgICAgICAgIGl0ZW1Qcm9kdXRvLk5vbWUg\nPSBwcm9kdXRvLk5vbWU7CiAgICAgICAgICAgIGl0ZW1Qcm9kdXRvLlZhbG9y\nID0gcHJvZHV0by5WYWxvcjsKICAgICAgICAgICAgaXRlbVByb2R1dG8uSW1h\nZ2VtID0gcHJvZHV0by5JbWFnZW07CgogICAgICAgICAgICB2YXIgcmVzcG9z\ndGEgPSBhd2FpdCBfY2FycmluaG9TZXJ2aWNlLkFkaWNpb25hckl0ZW1DYXJy\naW5obyhpdGVtUHJvZHV0byk7CgogICAgICAgICAgICByZXR1cm4gQ3VzdG9t\nUmVzcG9uc2UocmVzcG9zdGEpOwogICAgICAgIH0KCiAgICAgICAgW0h0dHBQ\ndXRdCiAgICAgICAgW1JvdXRlKCJjb21wcmFzL2NhcnJpbmhvL2l0ZW1zL3tw\ncm9kdXRvSWR9IildCiAgICAgICAgcHVibGljIGFzeW5jIFRhc2s8SUFjdGlv\nblJlc3VsdD4gQXR1YWxpemFySXRlbUNhcnJpbmhvKEd1aWQgcHJvZHV0b0lk\nLCBJdGVtQ2FycmluaG9EVE8gaXRlbVByb2R1dG8pCiAgICAgICAgewogICAg\nICAgICAgICB2YXIgcHJvZHV0byA9IGF3YWl0IF9jYXRhbG9nb1NlcnZpY2Uu\nT2J0ZXJQb3JJZChwcm9kdXRvSWQpOwoKICAgICAgICAgICAgYXdhaXQgVmFs\naWRhckl0ZW1DYXJyaW5obyhwcm9kdXRvLCBpdGVtUHJvZHV0by5RdWFudGlk\nYWRlKTsKICAgICAgICAgICAgaWYgKCFPcGVyYWNhb1ZhbGlkYSgpKSByZXR1\ncm4gQ3VzdG9tUmVzcG9uc2UoKTsKCiAgICAgICAgICAgIHZhciByZXNwb3N0\nYSA9IGF3YWl0IF9jYXJyaW5ob1NlcnZpY2UuQXR1YWxpemFySXRlbUNhcnJp\nbmhvKHByb2R1dG9JZCwgaXRlbVByb2R1dG8pOwoKICAgICAgICAgICAgcmV0\ndXJuIEN1c3RvbVJlc3BvbnNlKHJlc3Bvc3RhKTsKICAgICAgICB9CgogICAg\nICAgIFtIdHRwRGVsZXRlXQogICAgICAgIFtSb3V0ZSgiY29tcHJhcy9jYXJy\naW5oby9pdGVtcy97cHJvZHV0b0lkfSIpXQogICAgICAgIHB1YmxpYyBhc3lu\nYyBUYXNrPElBY3Rpb25SZXN1bHQ+IFJlbW92ZXJJdGVtQ2FycmluaG8oR3Vp\nZCBwcm9kdXRvSWQpCiAgICAgICAgewogICAgICAgICAgICB2YXIgcHJvZHV0\nbyA9IGF3YWl0IF9jYXRhbG9nb1NlcnZpY2UuT2J0ZXJQb3JJZChwcm9kdXRv\nSWQpOwoKICAgICAgICAgICAgaWYocHJvZHV0byA9PSBudWxsKQogICAgICAg\nICAgICB7CiAgICAgICAgICAgICAgICBBZGljaW9uYXJFcnJvUHJvY2Vzc2Ft\nZW50bygiUHJvZHV0byBpbmV4aXN0ZW50ZSEiKTsKICAgICAgICAgICAgICAg\nIHJldHVybiBDdXN0b21SZXNwb25zZSgpOwogICAgICAgICAgICB9CgogICAg\nICAgICAgICB2YXIgcmVzcG9zdGEgPSBhd2FpdCBfY2FycmluaG9TZXJ2aWNl\nLlJlbW92ZXJJdGVtQ2FycmluaG8ocHJvZHV0b0lkKTsKCiAgICAgICAgICAg\nIHJldHVybiBDdXN0b21SZXNwb25zZShyZXNwb3N0YSk7CiAgICAgICAgfQoK\nICAgICAgICBbSHR0cFBvc3RdCiAgICAgICAgW1JvdXRlKCJjb21wcmFzL2Nh\ncnJpbmhvL2FwbGljYXItdm91Y2hlciIpXQogICAgICAgIHB1YmxpYyBhc3lu\nYyBUYXNrPElBY3Rpb25SZXN1bHQ+IEFwbGljYXJWb3VjaGVyKFtGcm9tQm9k\neV0gc3RyaW5nIHZvdWNoZXJDb2RpZ28pCiAgICAgICAgewogICAgICAgICAg\nICB2YXIgdm91Y2hlciA9IGF3YWl0IF9wZWRpZG9TZXJ2aWNlLk9idGVyVm91\nY2hlclBvckNvZGlnbyh2b3VjaGVyQ29kaWdvKTsKICAgICAgICAgICAgaWYo\ndm91Y2hlciBpcyBudWxsKQogICAgICAgICAgICB7CiAgICAgICAgICAgICAg\nICBBZGljaW9uYXJFcnJvUHJvY2Vzc2FtZW50bygiVm91Y2hlciBpbnbDoWxp\nZG8gb3UgbsOjbyBlbmNvbnRyYWRvISIpOwogICAgICAgICAgICAgICAgcmV0\ndXJuIEN1c3RvbVJlc3BvbnNlKCk7CiAgICAgICAgICAgIH0KCiAgICAgICAg\nICAgIHZhciByZXNwb3N0YSA9IGF3YWl0IF9jYXJyaW5ob1NlcnZpY2UuQXBs\naWNhclZvdWNoZXJDYXJyaW5obyh2b3VjaGVyKTsKCiAgICAgICAgICAgIHJl\ndHVybiBDdXN0b21SZXNwb25zZShyZXNwb3N0YSk7CiAgICAgICAgfQoKICAg\nICAgICBwcml2YXRlIGFzeW5jIFRhc2sgVmFsaWRhckl0ZW1DYXJyaW5obyhJ\ndGVtUHJvZHV0b0RUTyBwcm9kdXRvLCBpbnQgcXVhbnRpZGFkZSkKICAgICAg\nICB7CiAgICAgICAgICAgIGlmIChwcm9kdXRvID09IG51bGwpIEFkaWNpb25h\nckVycm9Qcm9jZXNzYW1lbnRvKCJQcm9kdXRvIGluZXhpc3RlbnRlIik7CiAg\nICAgICAgICAgIGlmIChxdWFudGlkYWRlIDwgMSkgQWRpY2lvbmFyRXJyb1By\nb2Nlc3NhbWVudG8oJCJFc2NvbGhhIGFvIG1lbm9zIHVtYSB1bmlkYWRlIGRv\nIHByb2R1dG8ge3Byb2R1dG8uTm9tZX0iKTsKCiAgICAgICAgICAgIHZhciBj\nYXJyaW5obyA9IGF3YWl0IF9jYXJyaW5ob1NlcnZpY2UuT2J0ZXJDYXJyaW5o\nbygpOwogICAgICAgICAgICB2YXIgaXRlbUNhcnJpbmhvID0gY2FycmluaG8u\nSXRlbnMuRmlyc3RPckRlZmF1bHQocCA9PiBwLlByb2R1dG9JZC5FcXVhbHMo\ncHJvZHV0by5JZCkpOwogICAgICAgICAgICAKICAgICAgICAgICAgaWYoaXRl\nbUNhcnJpbmhvICE9IG51bGwgJiYgaXRlbUNhcnJpbmhvLlF1YW50aWRhZGUg\nKyBxdWFudGlkYWRlID4gcHJvZHV0by5RdWFudGlkYWRlRXN0b3F1ZSkKICAg\nICAgICAgICAgewogICAgICAgICAgICAgICAgQWRpY2lvbmFyRXJyb1Byb2Nl\nc3NhbWVudG8oJCJPIHByb2R1dG8ge3Byb2R1dG8uTm9tZX0gcG9zc3VpIHtw\ncm9kdXRvLlF1YW50aWRhZGVFc3RvcXVlfSB1bmlkYWRlcyBlbSBlc3RvcXVl\nLCB2b2PDqiBzZWxlY2lvbm91IHtxdWFudGlkYWRlfSIpOwogICAgICAgICAg\nICAgICAgcmV0dXJuOwogICAgICAgICAgICB9CgogICAgICAgICAgICBpZiAo\ncXVhbnRpZGFkZSA+IHByb2R1dG8uUXVhbnRpZGFkZUVzdG9xdWUpIEFkaWNp\nb25hckVycm9Qcm9jZXNzYW1lbnRvKCQiTyBwcm9kdXRvIHtwcm9kdXRvLk5v\nbWV9IHBvc3N1aSB7cHJvZHV0by5RdWFudGlkYWRlRXN0b3F1ZX0gdW5pZGFk\nZXMgZW0gZXN0b3F1ZSwgdm9jw6ogc2VsZWNpb25vdSB7cXVhbnRpZGFkZX0i\nKTsKICAgICAgICB9CiAgICB9Cn0=\n");
            Console.WriteLine(Encoding.UTF8.GetString(decoded));
            Console.ReadLine();
        }

        private static void GetCodeFiles(string rootDirectory, ref List<string> filesPaths)
        {
            var files = Directory.GetFiles(rootDirectory);

            foreach (var file in files)
            {
                if (file.EndsWith(".cs"))
                    filesPaths.Add(file);
            }

            var folders = Directory.GetDirectories(rootDirectory);

            foreach (var folder in folders)
            {
                if(!folder.EndsWith("obj"))
                    GetCodeFiles(folder, ref filesPaths);
            }
        }

        private static void ReadFile(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
        }

        private static byte[] Decode(string encoded)
        {
            return Convert.FromBase64String(encoded);
        }
    }
}