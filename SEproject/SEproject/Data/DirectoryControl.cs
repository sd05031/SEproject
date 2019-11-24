﻿using Newtonsoft.Json.Linq;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SEproject.Data
{
    class DirectoryControl
    {
        string path;
        ServerConnector serverconnector;
        private IList<File> Files;

        public DirectoryControl(ServerConnector sc)
        {
            path = ".";
            serverconnector = sc;
            get_list();
        }

        int removeFile(string filename)
        {
            string postdata = "path=" + path + "/" + filename;
            string rtext = serverconnector.POST("directory/remove", postdata);

            JObject jobject = JObject.Parse(rtext);
            int result = Int32.Parse(jobject["code"].ToString());

            return result;
        }

        public int removeDir()
        {
            if(path.Length < 2)
            {
                return -1;
            }

            string postdata = "path=" + path;
            string rtext = serverconnector.POST("directory/removedir", postdata);
            
            JObject jobject = JObject.Parse(rtext);
            int result = Int32.Parse(jobject["code"].ToString());

            return result;
        }

        int get_list()
        {
            Files = new List<File>();

            string postdata = "path=" + path;
            string rtext = serverconnector.POST("directory", postdata);

            JObject json = JObject.Parse(rtext);
            if (json["code"].ToString() == "0")
            {
                var msg = json["msg"];
                var directory_list = msg["dir"].ToString();
                var file_list = msg["file"].ToString();

                JArray directories = JArray.Parse(directory_list);
                JArray files = JArray.Parse(file_list);

                path = msg["path"].ToString().Substring(6);
                if ( path.Length > 2)
                {
                    Files.Add(new Data.File("..", 1));
                }
                for (int i = 0; i < directories.Count; i++)
                {
                    Files.Add(new Data.File(directories[i].ToString(), 1));
                }

                for (int i = 0; i < files.Count; i++)
                {
                    Files.Add(new Data.File(files[i].ToString(), 0));
                }
                return 0;
            }
            return -1;
        }

        public JObject download(string filepath)
        {
            string url = "http://nekop.kr:3000/api/v1/directory/download";
            string postdata = "path=" + filepath;
            byte[] bytearray = Encoding.UTF8.GetBytes(postdata);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytearray.Length;
            request.Timeout = 30 * 1000;
            request.Headers.Add("X-Access-Token", serverconnector.token);

            Stream datastream = request.GetRequestStream();
            datastream.Write(bytearray, 0, bytearray.Length);
            datastream.Close();

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            datastream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            string rtext = sr.ReadToEnd();

            JObject json = JObject.Parse(rtext);
            string value = json["msg"].ToString();
            json["msg"] = Base64Decoder(value);

            return json;
        }
        static private string Base64Decoder(string Base64text)
        {
            System.Text.Encoding encoding;
            encoding = System.Text.Encoding.UTF8;

            byte[] arr = System.Convert.FromBase64String(Base64text);
            return encoding.GetString(arr);

        }
        public JObject upload(FileData filedata)
        {
            string url = "http://nekop.kr:3000/api/v1/directory/upload";
            string boundary = "---UploadBoundaryWebDocker";
            byte[] Bbyte = Encoding.UTF8.GetBytes(boundary);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            //request.ContentLength = filedata.DataArray.Length;
            //request.Timeout = 30 * 1000;
            //request.Timeout = System.Threading.Timeout.Infinite;
            request.KeepAlive = true;
            request.Headers.Add("X-Access-Token", serverconnector.token);

            Stream datastream = request.GetRequestStream();

            //start
            datastream.Write(Bbyte, 0, Bbyte.Length);

            // main

            string headerTemplate1 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"";
            string headerTemplate2 = "Content-Type: {0}";
            
            // path
            string header1 = string.Format(headerTemplate1, "path", filedata.FileName);
            string header2 = string.Format(headerTemplate2, "text/plain");
            byte[] header_byte1 = Encoding.UTF8.GetBytes(header1);
            byte[] header_byte2 = Encoding.UTF8.GetBytes(header2);
            datastream.Write(header_byte1, 0, header_byte1.Length);
            datastream.Write(header_byte1, 0, header_byte2.Length);

            byte[] path_byte = Encoding.UTF8.GetBytes(path + "/" + filedata.FileName);
            datastream.Write(path_byte, 0, path_byte.Length);

            datastream.Write(Bbyte, 0, Bbyte.Length);
            // data

            header1 = string.Format(headerTemplate1,"data",filedata.FileName);
            header2 = string.Format(headerTemplate2, "application/octet-stream");
            header_byte1 = Encoding.UTF8.GetBytes(header1);
            header_byte2 = Encoding.UTF8.GetBytes(header2);
            datastream.Write(header_byte1, 0, header_byte1.Length);
            datastream.Write(header_byte1, 0, header_byte2.Length);

            datastream.Write(filedata.DataArray, 0, filedata.DataArray.Length);

            // main - file
            /*FileStream filestream = new FileStream(filedata.FilePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesread = 0;
            while ((bytesread = filestream.Read(buffer, 0, buffer.Length)) != 0)
            {
                datastream.Write(buffer, 0, bytesread);
            }*/

            //end
            datastream.Write(Bbyte, 0, Bbyte.Length);
            datastream.Close();

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            datastream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            string rtext = sr.ReadToEnd();

            JObject json = JObject.Parse(rtext);
            
            return json;
        }
        public void movepath(string dir)
        {
            if (dir == "..")
            {
                var splited = path.Split('/');
                string newpath = "";
                for ( int i = 0; i < splited.Length - 1; i ++)
                {
                    newpath += i == 0 ? splited[i] : "/" + splited[i];
                }
                path = newpath;
            }
            else
            {
                path = path + "/" + dir;
            }
            get_list();
        }

        public IList<File> GetFiles()
        {
            return Files;
        }
        public string getpath()
        {
            return path;
        }
    }
}
