using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPI.Models
{
    public class Autor
    {
        public Autor()
        {

        }
        public Autor(string nomeCompleto)
        {
            NomeCompleto = nomeCompleto;
            string[] Nomes = NomeCompleto.Split(" ", StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < Nomes.Length; i++)
            {
                if (i == 0)
                    Nome = (Nomes.Length == 1) ? Nomes[i].ToUpper() : Nomes[i];
                else if (i == 1)
                {
                    if (NomesDescartados.Contains(Nomes[i].ToLower()))
                        NomeAux = Nomes[i].ToLower();
                    else
                        Sobrenome = Sobrenome + " " + Nomes[i].ToUpper();
                }
                else
                {
                    var name = NomesDescartados.Contains(Nomes[i].ToLower()) ? Nomes[i].ToLower() : Nomes[i].ToUpper();
                    Sobrenome = Sobrenome + " " + name;
                }

            }
            NomeExibicao = Sobrenome + ", " + DefineLetraMaiuscula(Nome) + " " + NomeAux;

        }
        public string DefineLetraMaiuscula(string palavra)
        {
            return palavra.Length > 1 ? char.ToUpper(palavra[0]) + palavra.Substring(1).ToLower() : palavra.ToUpper();
        }

    
        private readonly string[] NomesDescartados = new[]
        {
            "da",
            "de",
            "do",
            "das",
            "dos"
        };

        [Key]
        public int IdAutor { get; set; }
        public string Nome { get; set; }

        public string NomeAux { get; set; }

        public string Sobrenome { get; set; }

        public string NomeCompleto { get; set; }
        public string NomeExibicao { get; set; }
    }
}
