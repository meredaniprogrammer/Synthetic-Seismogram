using Newtonsoft.Json;
using Synthetic_Seismogram.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Synthetic_Seismogram.Components
{
    public class UtilityHelper
    {
        public  string getPythonBase()
        {
            return "http://localhost:54555";
          
        }
        public async Task<string[]> getSyntheticFromPython(float dt,float[] xps,float[] fps)
        {
           
            using (var client = new HttpClient())
            {
                var baseUri = getPythonBase() + "/api/";
                var stringxp = "";
                var stringfp = "";
                var count1 = 0;
                var count2 = 0;
                foreach (var xp in xps)
                {
                    count1++;
                    if (count1==xps.Length)
                    {
                        var xpstring = $"{xp}";
                        stringxp += xpstring;
                    }
                    else
                    {
                        var xpstring = $"{xp},";
                        stringxp += xpstring;
                    }
                   
                } 
                foreach (var fp in fps)
                {
                    count2++;
                    if (count2 == fps.Length)
                    {
                        var xpstring = $"{fp}";
                        stringfp += xpstring;
                    }
                    else
                    {
                        var xpstring = $"{fp},";
                        stringfp += xpstring;
                    }
                   
                }
                var myparams= $"?dt={dt}&xp={stringxp}&fp={stringfp}";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync($"synthetic{myparams}");
          
                if (response.IsSuccessStatusCode)   
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    char[] seperator = { ',' };
                    String[] commasplit = responseJson.Split(seperator, StringSplitOptions.None);
                    return commasplit;
                }
                return null;
            }
        }
    }
}
