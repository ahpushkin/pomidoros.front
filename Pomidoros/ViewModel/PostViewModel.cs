using System;
using System.IO;
using System.Net;
using System.Text;

namespace Pomidoros.ViewModel
{
    public class PostViewModel
    {

        public PostViewModel()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://138.201.153.220/api/swagger/");

            var postData = "thing1=" + Uri.EscapeDataString("phone");
            postData += "&thing2=" + Uri.EscapeDataString("psword");
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
