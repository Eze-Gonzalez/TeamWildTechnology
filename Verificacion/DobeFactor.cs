using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Verify.V2;
using Twilio.Rest.Verify.V2.Service;

namespace Verificacion
{
    public class DobeFactor
    {
        //VE371a92a8cf881a63a0f6ab05e8025e0b --> 
        //VA77c18f2ab5674136460d84fb4c868eb0 --> Service SID --> Twilio
        //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 --> ApiKey SendGrid
        public void iniciarAuthy()
        {
            try
            {
                string accountSid = Environment.GetEnvironmentVariable("AC92c3ba2e330da62e0ae77221b708f3a0");
                string authToken = Environment.GetEnvironmentVariable("c3727ef01f9d5840aa17fd0a11e2048b");

                TwilioClient.Init(accountSid, authToken);

                var service = ServiceResource.Create(friendlyName: "Verificación de Team Wild Technology");

                Console.WriteLine(service.Sid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void crearVerificacion(string numerotelefono, string metodo)
        {
            try
            {
                var verification = VerificationResource.Create(
                to: numerotelefono,
                channel: metodo,
                pathServiceSid: "VAXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Console.WriteLine(verification.Status);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
