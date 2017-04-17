using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Mvc;
using Newtonsoft.Json;
using Comunes;
using System.Net.Http.Headers;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        readonly string uri = "http://localhost:51092/api/superheroes";

        public Form1()
        {
            InitializeComponent();
            //CrearUno();
            ConsultarUno();
            Eliminar();
            ConsultarUno();
        }

        public void ConsultarUno()
        {
            using (var client = new HttpClient())
            {
                var model = JsonConvert.DeserializeObject<List<SuperHeroesModel>>(Task.Run(() => client.GetStringAsync(uri)).Result);
            }
        }
        
        public void CrearUno()
        {
            var superHeroe = JsonConvert.SerializeObject(new SuperHeroesModel { Id = 2, Nombre = "otro", Sexo = "F", SuperPoder = "Rapido" });
            var content = new StringContent(superHeroe, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var respuesta = Task.Run(() => client.PostAsync(uri, content)).Result;
                
            }
        }

        public void Actualizar()
        {
            var superHeroe = new SuperHeroesModel { Id = 1, Nombre = "actualizado", Sexo = "M", SuperPoder = "Rico" };
            var superHeroeSerializado = JsonConvert.SerializeObject(superHeroe);

            var inputMessage = new HttpRequestMessage
            {
                Content = new StringContent(superHeroeSerializado, Encoding.UTF8, "application/json")
            };
            inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var client = new HttpClient())
            {
                var respuesta = Task.Run(() => client.PutAsync(uri + "/" + superHeroe.Id, inputMessage.Content)).Result;
            }
        }

        public void Eliminar()
        {
            using (var client = new HttpClient())
            {
                var respuesta = Task.Run(() => client.DeleteAsync(uri + "/" + 1)).Result;
            }
        }
    }
}
