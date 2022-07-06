﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenUtils
{
    public static class TokenGeneration
    {
        public static long GenerateToken(string token, int numberofRotations)
        {
            try
            {
                var array = token.ToString().ToCharArray().Select(Convert.ToInt32).ToArray();
                int auxVariable;
                for (int i = 0; i < numberofRotations; i++)
                {
                    for (int j = 0; j < array.Length - 1; j++)
                    {
                        auxVariable = array[0];
                        array[0] = array[j + 1];
                        array[j + 1] = auxVariable;
                    }
                }
                string[] result = array.Select(i => i.ToString()).ToArray();
                return Convert.ToInt64(String.Join("", result));
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred during the token generation process: " + ex.Message);
            }
           
        }
    }
}
