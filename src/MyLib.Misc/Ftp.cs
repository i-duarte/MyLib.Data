using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MyLib.Misc
{
    public static class Ftp
    {
        public static bool Test(
            string ip,
            string login,
            string password
            )
        {
            try
            {
                var pathServidor = ip;

                if (!pathServidor.StartsWith("ftp://", StringComparison.Ordinal))
                {
                    pathServidor = "ftp://" + pathServidor;
                }

                if (
                    WebRequest.Create(new Uri(pathServidor))
                        is FtpWebRequest req
                )
                {
                    req.Credentials = new NetworkCredential(login, password);
                    req.Proxy = null;
                    req.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                    req.UsePassive = true;
                    req.UseBinary = true;
                    req.KeepAlive = false;

                    req.GetResponse();

                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        public static IEnumerable<string> GetListaArchivos(
            string ip,
            string directorio,
            string login,
            string password
            )
        {
            var pathServidor = "";
            // Get the object used to communicate with the server.
            if (!pathServidor.StartsWith("ftp://", StringComparison.Ordinal))
            {
                pathServidor = "ftp://";
            }
            pathServidor += ip;
            
            if (
                WebRequest.Create(
                    pathServidor
                    + ("/%2F/" + directorio).Replace("//", "/")
                ) is FtpWebRequest request
            )
            {
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Proxy = null;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials
                    = new NetworkCredential(login, password);

                var response
                    = request.GetResponse() as FtpWebResponse;

                var responseStream
                    = response.GetResponseStream();

                if (responseStream != null)
                {
                    var reader
                        = new StreamReader(responseStream);
                    string dir
                        = reader.ReadToEnd();

                    string[] archivos
                        = dir.Split(new[] { "\r\n" }, StringSplitOptions.None);

                    foreach (var archivo in archivos)
                    {
                        yield return archivo;
                    }

                    reader.Close();
                }
                response.Close();
            }
        }

        public static bool Descargar(
            string pathServidor,
            string dirServidor,
            string archivo,
            string login,
            string password,
            string pathArchivoLocal
            )
        {
            try
            {

                if (
                    WebRequest.Create(
                            (pathServidor.Substring(0, 6) == "ftp://" ? "" : "ftp://")
                            + pathServidor
                            + ("/%2F/" + dirServidor + "/" + archivo).Replace("//", "/")
                    ) is FtpWebRequest request
                )
                {
                    request.Timeout = 5000;
                    request.Method
                        = WebRequestMethods.Ftp.DownloadFile;
                    request.Proxy = null;
                    request.Credentials
                        = new NetworkCredential(login, password);
                    request.UsePassive = true;
                    request.UseBinary = true;
                    request.KeepAlive = false;

                    var response
                        = (FtpWebResponse)request.GetResponse();

                    const int bufferSize = 2048;

                    var buffer = new byte[bufferSize];

                    var outputStream
                        = File.OpenWrite(pathArchivoLocal);

                    var ftpStream
                        = response.GetResponseStream();

                    if (ftpStream != null)
                    {
                        var readCount
                            = ftpStream.Read(buffer, 0, bufferSize);

                        while (readCount > 0)
                        {
                            outputStream
                                .Write(buffer, 0, readCount);

                            readCount
                                = ftpStream.Read(buffer, 0, bufferSize);
                        }
                        ftpStream.Close();
                    }
                    outputStream.Flush();
                    outputStream.Close();
                    response.Close();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System
                    .Diagnostics
                    .Debug
                    .WriteLine(ex.Message);

                System
                    .Diagnostics
                    .Debug
                    .WriteLine(ex.StackTrace);

                return false;
            }
        }

        public static bool Enviar(
            string ip,
            string dirServidor,
            string login,
            string password,
            string archivo
            )
        {
            try
            {
                var pathServidor = "";
                // Get the object used to communicate with the server.
                if (!pathServidor.StartsWith("ftp://", StringComparison.Ordinal))
                {
                    pathServidor = "ftp://";
                }

                pathServidor += ip;

                if (
                    WebRequest.Create(
                         pathServidor
                         + ("/%2F/" + dirServidor)
                                .Replace("//", "/")
                         + "/"
                         + Path.GetFileName(archivo)
                    ) is FtpWebRequest request
                )
                {
                    request.Timeout = 15000;
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Proxy = null;
                    request.Credentials = new NetworkCredential(login, password);
                    request.UsePassive = true;
                    request.UseBinary = true;
                    request.KeepAlive = false;

                    var stream = File.OpenRead(archivo);
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);

                    var reqStream = request.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                    stream.Close();

                    var response = (FtpWebResponse)request.GetResponse();
                    response.Close();

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

       
    }
}
