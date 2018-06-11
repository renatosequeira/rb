using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rainbow.Backend.Algoritmos
{
    public class CalculoDeQualificacao
    {
        private static int LimiteIdadeInferior = 30;
        private static int LimiteIdadeSuperior = 76;

        public bool CalcularQualificacao(int idade, int estadoCivil, string telemovel)
        {
            bool response = false;

            if(idade >= LimiteIdadeInferior && idade <= LimiteIdadeSuperior)
            {
                if(estadoCivil != 5 && !string.IsNullOrEmpty(telemovel))
                {
                    response = true;
                }
            }

            return response;
        }
    }
}