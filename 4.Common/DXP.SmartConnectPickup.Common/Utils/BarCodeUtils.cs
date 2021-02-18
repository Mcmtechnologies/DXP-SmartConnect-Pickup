using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DXP.SmartConnectPickup.Common.Utils
{
    public static class BarCodeUtils
    {
        public static Stream GetBarCodeStream(string upc, decimal finalRetailPrice, int width, int height)
        {
            try
            {
                BarcodeLib.Barcode barcodeLib = new BarcodeLib.Barcode();
                barcodeLib.IncludeLabel = true;
                string barCode = GetBarcode(upc, finalRetailPrice);
                Image image;

                if (barCode.Length == 12)
                {
                    image = barcodeLib.Encode(BarcodeLib.TYPE.UPCA, barCode, Color.Black, Color.White, width, height);
                }
                else
                {
                    image = barcodeLib.Encode(BarcodeLib.TYPE.EAN13, barCode, Color.Black, Color.White, width, height);
                }

                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                return stream;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetUpc12(string upc11)
        {
            return upc11 + CalculateCheckDigitForGTIN12(upc11);
        }

        public static string GetUpc13(string upc12)
        {
            return upc12 + CalculateCheckDigitForGTIN13(upc12);
        }

        public static bool IsValidUpcCheckDigit(string upc)
        {
            if (string.IsNullOrEmpty(upc))
            {
                return false;
            }

            if (upc.Length == 12)
            {
                string upc11 = upc.Substring(0, 11);
                string upc12 = upc11 + CalculateCheckDigitForGTIN12(upc11);

                return upc == upc12;
            }

            if (upc.Length == 13)
            {
                string upc12 = upc.Substring(0, 12);
                string upc13 = upc12 + CalculateCheckDigitForGTIN13(upc12);

                return upc == upc13;
            }

            return true;
        }

        public static bool IsValidUpcAndPrice(string upc, decimal finalRetailPrice)
        {
            string finalRetailPriceString = finalRetailPrice.ToString().Replace(".", "");

            if (upc.Length == 6 && finalRetailPriceString.Length > 4)
            {
                return false;
            }

            return true;
        }

        public static string GetBarcode(string upc, decimal finalRetailPrice)
        {
            // 6 digit items with 4 digit price, type 2 barcodes.
            // 7 digit items with 4 and 5 digit prices, need to PAD the price value to ensure a 5 digit value, type 20 barcode
            // 12 digits items, need to STAY 12 digits and NOT get a check digit added, treat at type 2 barcode
            // 13 digit items, need to STAY 13 digits and NOT get any other logic applied, type 20 barcode
            // In Online Ordering there won't be any product with price >=$1000.
            // They will only provide 12, 13, 7 or 6 so if it not in those 2 leave the barcode blank.
            // Final retail price including plus cost of any options AND not including tax.

            if (upc.Length == 12 || upc.Length == 13)
            {
                return upc;
            }

            string formattedRetailPrice = GetFinalRetailPrice(finalRetailPrice, upc.Length);
            string code = upc + formattedRetailPrice;
            string checkDigit = code.Length == 11 ? CalculateCheckDigitForGTIN12(code) : CalculateCheckDigitForGTIN13(code);
            return code + checkDigit;
        }

        public static string GetFinalRetailPrice(decimal finalRetailPrice, int upcLength)
        {
            string result = finalRetailPrice.ToString().Replace(".", "");

            if (upcLength == 6)
            {
                if (result.Length == 3)
                {
                    result = $"0{result}";
                }
                else if (result.Length == 2)
                {
                    result = $"00{result}";
                }

                if (result.Length == 4)
                {
                    int priceCheckDigit = CheckDigitCalculationForTheFourDigitPriceField(result);
                    return priceCheckDigit.ToString() + result;
                }

                return result;
            }

            // UPC Length is 7
            if (result.Length == 4)
            {
                result = $"0{result}";
            }
            if (result.Length == 3)
            {
                result = $"00{result}";
            }
            else if (result.Length == 2)
            {
                result = $"000{result}";
            }

            return result;
        }

        public static string CalculateCheckDigitForGTIN12(string code)
        {
            // Document: https://www.gs1.org/services/how-calculate-check-digit-manually
            // https://stackoverflow.com/questions/10143547/how-do-i-validate-a-upc-or-ean-code

            int[] a = new int[11];
            a[0] = int.Parse(code[0].ToString()) * 3;
            a[1] = int.Parse(code[1].ToString());
            a[2] = int.Parse(code[2].ToString()) * 3;
            a[3] = int.Parse(code[3].ToString());
            a[4] = int.Parse(code[4].ToString()) * 3;
            a[5] = int.Parse(code[5].ToString());
            a[6] = int.Parse(code[6].ToString()) * 3;
            a[7] = int.Parse(code[7].ToString());
            a[8] = int.Parse(code[8].ToString()) * 3;
            a[9] = int.Parse(code[9].ToString());
            a[10] = int.Parse(code[10].ToString()) * 3;

            int sum = a[0] + a[1] + a[2] + a[3] + a[4] + a[5] + a[6] + a[7] + a[8] + a[9] + a[10];
            int checkDigit = (10 - (sum % 10)) % 10;

            return checkDigit.ToString();
        }

        public static string CalculateCheckDigitForGTIN13(string code)
        {
            // Document: https://www.gs1.org/services/how-calculate-check-digit-manually
            // https://stackoverflow.com/questions/10143547/how-do-i-validate-a-upc-or-ean-code

            int[] a = new int[12];
            a[0] = int.Parse(code[0].ToString());
            a[1] = int.Parse(code[1].ToString()) * 3;
            a[2] = int.Parse(code[2].ToString());
            a[3] = int.Parse(code[3].ToString()) * 3;
            a[4] = int.Parse(code[4].ToString());
            a[5] = int.Parse(code[5].ToString()) * 3;
            a[6] = int.Parse(code[6].ToString());
            a[7] = int.Parse(code[7].ToString()) * 3;
            a[8] = int.Parse(code[8].ToString());
            a[9] = int.Parse(code[9].ToString()) * 3;
            a[10] = int.Parse(code[10].ToString());
            a[11] = int.Parse(code[11].ToString()) * 3;

            int sum = a[0] + a[1] + a[2] + a[3] + a[4] + a[5] + a[6] + a[7] + a[8] + a[9] + a[10] + a[11];
            int checkDigit = (10 - (sum % 10)) % 10;

            return checkDigit.ToString();
        }

        public static int CalculateWeightingFactor2Minus(int input)
        {
            // Calculation rule: The digit is multiplied by 2. If the result has two digits, the tens digit is
            // subtracted from the units digit.The units digit resulting is the weighted product.

            int multipliedBy2 = input * 2;

            if (multipliedBy2.ToString().Length == 1)
            {
                return multipliedBy2;
            }
            else
            {
                string multipliedBy2String = multipliedBy2.ToString();
                int tensDigit = int.Parse(multipliedBy2String.Substring(0, 1));
                int unitsDigit = int.Parse(multipliedBy2String.Substring(1, 1));

                return (10 + (unitsDigit - tensDigit)) % 10;
            }
        }

        public static int CalculateWeightingFactor3(int input)
        {
            // Calculation rule: The digit is multiplied by 3. The units digit of the result is the weighted
            // product.

            int multipliedBy3 = input * 3;
            string multipliedBy3String = multipliedBy3.ToString();

            return int.Parse(multipliedBy3String.Substring(multipliedBy3String.Length - 1));
        }

        public static int CalculateWeightingFactor5Minus(int input)
        {
            // Calculation rule: The digit is multiplied by 5. The tens digit of the result is subtracted from
            // the result. The units digit of the result of this subtraction is the weighted product.

            int multipliedBy5 = input * 5;

            if (multipliedBy5.ToString().Length == 1)
            {
                return multipliedBy5;
            }
            else
            {
                string multipliedBy5String = multipliedBy5.ToString();
                int tensDigit = int.Parse(multipliedBy5String.Substring(0, 1));
                int subtractedNumber = tensDigit - multipliedBy5;

                if (subtractedNumber < 0)
                {
                    subtractedNumber *= -1;
                }

                return subtractedNumber % 10;
            }
        }

        public static int CalculateWeightingFactor5Plus(int input)
        {
            // Calculation rule: The digit is multiplied by 5. The units digit and the tens digit of the result
            // are added together.The result of this sum is the weighted product.

            int multipliedBy5 = input * 5;

            if (multipliedBy5.ToString().Length == 1)
            {
                return multipliedBy5;
            }
            else
            {
                string multipliedBy5String = multipliedBy5.ToString();
                int tensDigit = int.Parse(multipliedBy5String.Substring(0, 1));
                int unitsDigit = int.Parse(multipliedBy5String.Substring(1, 1));

                return unitsDigit + tensDigit;
            }
        }

        public static int CheckDigitCalculationForTheFourDigitPriceField(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Length == 4)
            {
                // Calculation step 1: Determine the weighted product for each number in Positions one to four
                // according to the assigned weighting factors. 
                int weightedProduct1 = CalculateWeightingFactor2Minus(int.Parse(input[0].ToString()));
                int weightedProduct2 = CalculateWeightingFactor2Minus(int.Parse(input[1].ToString()));
                int weightedProduct3 = CalculateWeightingFactor3(int.Parse(input[2].ToString()));
                int weightedProduct4 = CalculateWeightingFactor5Minus(int.Parse(input[3].ToString()));

                // Calculation step 2: Add the products of step 1.
                int sum = weightedProduct1 + weightedProduct2 + weightedProduct3 + weightedProduct4;

                // Calculation step 3: Multiply the result of step 2 by the factor 3. The units digit of the result is the Check
                // Digit.
                string result = (sum * 3).ToString();

                return int.Parse(result.Substring(result.Length - 1));
            }

            return -1;
        }
    }
}
