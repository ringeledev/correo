using System;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace EnvioCorreoDotNet6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lee el contenido del archivo appsettings.json
            string json = System.IO.File.ReadAllText("config.json");

            // Analiza el contenido JSON en un objeto dynamic
            dynamic config = JsonSerializer.Deserialize<dynamic>(json);

            string correoEnviante = config["CorreoEnviante"];
            string contraseña = config["Contraseña"];

            try
            {
                using (MailMessage correos = new MailMessage())
                {
                    using (SmtpClient envio = new SmtpClient())
                    {
                        correos.To.Clear();
                        correos.Attachments.Clear();

                        correos.Subject = "Prueba de correo con C#.NET";
                        correos.Body = "Esto es el cuerpo del correo";
                        correos.IsBodyHtml = false;
                        correos.To.Add("ringeledev@gmail.com"); // Destinatario

                        correos.From = new MailAddress(correoEnviante);
                        envio.Credentials = new NetworkCredential(correoEnviante, contraseña);

                        // Configuración de Gmail
                        envio.Host = "smtp.gmail.com";
                        envio.Port = 587;
                        envio.EnableSsl = true;

                        envio.Send(correos);
                        Console.WriteLine("Éxito - Correo Enviado Correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
        }
    }
}
