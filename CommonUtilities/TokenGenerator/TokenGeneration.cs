using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenUtils
{
    public static class TokenGeneration
    {
        public static long GenerateToken(string cardNumber, int numberofRotations)
        {
            try
            {
                var array = cardNumber.Select(x => x - 48).ToArray();

                int auxVariable;
                for (int i = 0; i < numberofRotations; i++)
                {
                    for (int j = 0; j < array.Length - 1; j++)
                    {
                        auxVariable = array[0];
                        array[0] = array[j + 1];
                        array[j + 1] = (byte)auxVariable;
                    }
                }
                return Convert.ToInt64(String.Join("", array));
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred during the token generation process: " + ex.Message);
            }
           
        }
    }
}
