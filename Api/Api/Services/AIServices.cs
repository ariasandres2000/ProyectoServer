using System.Net;

namespace Api.Services
{
    public class AIServices
    {
        private HttpClient client = new HttpClient();
        private string tokenAI = "";
        private string organizacion = "";       

        public string Imagen(string instruccion, int cantidad, string tamano)
        {
            var url = "https://api.openai.com/v1/images/generations";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + tokenAI);
            //request.Headers.Add("OpenAI-Organization", organizacion);

            var valor = new
            {
                prompt = instruccion,
                n = cantidad,
                size = tamano
            };

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(valor);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Editar(string entrada, string instruccion)
        {
            var url = "https://api.openai.com/v1/edits";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + tokenAI);
            //request.Headers.Add("OpenAI-Organization", organizacion);

            var valor = new
            {
                model = "text-davinci-edit-001",
                input = entrada,
                instruction = instruccion
            };

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(valor);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Terminacion(string entrada)
        {
            var url = "https://api.openai.com/v1/completions";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + tokenAI);
            //request.Headers.Add("OpenAI-Organization", organizacion);

            var valor = new
            {
                model = "text-davinci-003",
                prompt = entrada,
                max_tokens = 7,
                temperature = 0
            };

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(valor);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
