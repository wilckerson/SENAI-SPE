using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;

namespace Common.Email
{
    public static class EmailHelper
    {
        public static void DispararEmail(string nome, string titulo, string email, string telefone, string conteudo)
        {
            String headEmailBody = "Nome: {0} <br /> Email: {1} <br /> Telefone: {2} <br />";
            SmtpClient cliente = new SmtpClient(ConfigurationSettings.AppSettings["SMTPServerName"]);
            cliente.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["EmailPadraoEnvio"],
                                                                   ConfigurationSettings.AppSettings["EmailPadraoEnvioSenha"]);
            cliente.Port = int.Parse(ConfigurationSettings.AppSettings["SMTPServerPort"]);

            MailAddress from = new MailAddress(ConfigurationSettings.AppSettings["EmailPadraoEnvio"]);
            MailAddress to = new MailAddress(email);
            cliente.EnableSsl = true;
            MailMessage mensagem = new MailMessage(from, to);
            mensagem.IsBodyHtml = true;
            mensagem.Subject = "[" + nome + "] - " + titulo;
            mensagem.Body = String.Format(headEmailBody, nome, email, telefone) + conteudo;

            try
            {
                cliente.Send(mensagem);
                Console.WriteLine("Email enviado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ao enviar o email para o suporte. Detalhes: " + ex.Message);
            }
        }

        public static void DispararEmailSenha(string email, string nome, Guid guid)
        {
            String headEmailBody = "Prezado(a) {0}, <br /><br /> Foi solicitada uma nova senha para acessar o site do programa Profissão Futuro GDF.<br/><br/>" +
             "Para modificar sua senha acesse o seguinte link: <a href=\"http://www.profissaofuturogdf.com.br/Home/ModificarSenha?Acesso={1}\">http://www.profissaofuturogdf.com.br/Home/ModificarSenha?Acesso={1}</a><br/><br/>" +
             "Em caso de dúvida, entre em <a href=\"http://www.profissaofuturogdf.com.br/Home/Contato/\">contato conosco</a>.<br/><br/>" +
             "Esse acesso para modificar a senha não será mais válido em 20 minutos. Se você não soliciou a mudança de senha, ignore este e-mail, por favor.<br/><br/>" +
             "Atenciosamente, <br/><br/>" +
             "Equipe Profissão Futuro GDF";

            SmtpClient cliente = new SmtpClient(ConfigurationSettings.AppSettings["SMTPServerName"]);

            cliente.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["EmailPadraoEnvio"],
                                                                   ConfigurationSettings.AppSettings["EmailPadraoEnvioSenha"]);
            cliente.Port = int.Parse(ConfigurationSettings.AppSettings["SMTPServerPort"]);

            MailAddress from = new MailAddress(ConfigurationSettings.AppSettings["EmailPadraoEnvioSENAI"]);
            MailAddress to = new MailAddress(email);

            MailMessage mensagem = new MailMessage(from, to);
            mensagem.IsBodyHtml = true;
            mensagem.Subject = "Programa Profissão Futuro: Resetar Senha.";
            mensagem.Body = string.Format(headEmailBody, nome, guid.ToString());

            try
            {
                cliente.Send(mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DispararEmailSenhaFlexiTonus(string email, string nome, string login)
        {
            String headEmailBody = "Prezado(a) {0}, <br /><br /> Foi criado um Login e senha para acessar o Sistema Fit30.<br/><br/>" +
              "Seu Login é : {1}<br/>"+
              "E a sua senha é o CPF informado no momento da matricula"+
             "Equipe FIT30";

            SmtpClient cliente = new SmtpClient(ConfigurationSettings.AppSettings["SMTPServerName"]);

            cliente.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["EmailPadraoEnvio"],
                                                                   ConfigurationSettings.AppSettings["EmailPadraoEnvioSenha"]);
            cliente.Port = int.Parse(ConfigurationSettings.AppSettings["SMTPServerPort"]);
            cliente.EnableSsl = true;

            MailAddress from = new MailAddress(ConfigurationSettings.AppSettings["EmailPadraoEnvioFlexiTonus"]);
            MailAddress to = new MailAddress(email);

            MailMessage mensagem = new MailMessage(from, to);
            mensagem.IsBodyHtml = true;
            mensagem.Subject = "Login e Senha Academia Fit30";
            mensagem.Body = string.Format(headEmailBody, nome, login);

            try
            {
                cliente.Send(mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DispararEmailErrorInscricao(object modelToSerializer, string nomeCandidato, Exception exception)
        {
            String headEmailBody = "Data: {0} - Error Inscrição: <br/><br/>" +
                "Mensagem: {1} <br/><br/>" +
                "InnerException: {2} <br/><br/>" +
                "StackTrace: {3} <br /><br />" +
                "Object Serialized: {4}";

            SmtpClient cliente = new SmtpClient(ConfigurationSettings.AppSettings["SMTPServerName"]);

            cliente.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["EmailPadraoEnvio"],
                                                                   ConfigurationSettings.AppSettings["EmailPadraoEnvioSenha"]);
            cliente.Port = int.Parse(ConfigurationSettings.AppSettings["SMTPServerPort"]);

            MailAddress from = new MailAddress(ConfigurationSettings.AppSettings["EmailPadraoEnvioSENAI"]);
            MailAddress to = new MailAddress("bruno.kenj@idevelop.com.br");

            MailMessage mensagem = new MailMessage(from, to);
            mensagem.Bcc.Add("jose.roberto@idevelop.com.br");
            mensagem.Bcc.Add("bruno.souza@idevelop.com.br");
            mensagem.Bcc.Add("ednaldo.souza@idevelop.com.br");
            mensagem.IsBodyHtml = true;
            mensagem.Subject = "Programa Profissão Futuro: Error Inscrição - [" + nomeCandidato + "]";
            mensagem.Body = String.Format(headEmailBody, DateTime.UtcNow.ToString(), exception.Message, exception.InnerException, exception.StackTrace, SerializeToString(modelToSerializer));

            try
            {
                cliente.Send(mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }
    }

}
