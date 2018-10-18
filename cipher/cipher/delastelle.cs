using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cipher
{
    class delastelle
    {
        private string[,] matrix = new string[5, 5];

        public string[] get_matrix()
        {
            string[] raw = new string[25];
            int cnt = 0;
            for(int i = 0; i< 5; i++)
            {
                for(int j = 0; j<5; j++)
                {
                    raw[cnt] = matrix[i, j].ToString();
                    cnt++;
                }
            }
            return raw;
        }

        public void set_matrix(string password)
        { 
            string all_leters = "abcdefghijklmnopqrstuvwxyz";
            all_leters = all_leters.Insert(0, password);
            all_leters.ToLower();
            all_leters = all_leters.Replace('i', 'j');
            string final = new string(all_leters.ToCharArray().Distinct().ToArray());

            int cnt = 0;
            for(int i = 0; i< 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(final[cnt] == 'j')
                    {
                        matrix[i, j] = "i,j";
                    }
                    else
                    {
                        matrix[i, j] = final[cnt].ToString();
                    }
                    cnt++;
                }
            }
        }

        private Tuple<int, int> find(string tmp)
        {
            for(int i = 0; i < 5; i++)
                for(int j = 0; j < 5; j++)
                    if(matrix[i,j] == tmp || (matrix[i,j] == "i,j" && (tmp == "i" || tmp == "j")))
                        return new Tuple<int, int>(j+1, i+1); // column, line

            return new Tuple<int, int>(-1,-1);
        }

        public string[] tokens_raw(string text)
        {
            List<string> list = new List<string>();
            string num = "";
            string spec = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] >= 'a' && text[i] <= 'z')
                {
                    list.Add(spec);
                    spec = "";
                    num += text[i];
                }
                else
                {
                    list.Add(num);
                    num = "";
                    spec += text[i];
                }
            }
            list.Add(num);
            list.Add(spec);
            list.RemoveAll(p => string.IsNullOrEmpty(p));
            return list.ToArray();
        }


        #region poziomo
        public string poziomo_przejsciowa(string text)
        {
            List<Tuple<int, int>> arr = new List<Tuple<int, int>>();
            for(int i = 0; i<text.Length; i++)
            {
                arr.Add(find(text[i].ToString()));
            }
            string left = "";
            string right = "";
            foreach (var elem in arr)
            {
                left += elem.Item1.ToString();
                right += elem.Item2.ToString();
            }
            

            return left+right;
        }

        public string poziomo_szyforwanie(string text)
        {
            string tmp = poziomo_przejsciowa(text);
            string to_return = "";

            for(int i = 0; i < tmp.Length; i+=2)
            {
                string temp = matrix[Int32.Parse(tmp[i + 1].ToString()) - 1, Int32.Parse(tmp[i].ToString()) - 1];
                if (temp == "i,j")
                    temp = "i";
                to_return += temp;
            }

            return to_return;
        }

        public string pozimo_przejscowa_rozszufruj(string text)
        {
            string to_return = "";

            for (int i = 0; i < text.Length; i++)
            {
                Tuple<int, int> tmp = find(text[i].ToString());
                to_return += tmp.Item1.ToString() + tmp.Item2.ToString();
            }

            return to_return;
        }

        public string poziomo_rozszyfruj(string text)
        {
            string tmp = pozimo_przejscowa_rozszufruj(text);
            string to_return = "";

            string left = tmp.Substring(0, tmp.Length/2);
            string right = tmp.Substring(tmp.Length / 2, tmp.Length - tmp.Length / 2);

            for(int i = 0; i < left.Length; i++)
            {
                string temp = matrix[Int32.Parse(right[i].ToString()) - 1, Int32.Parse(left[i].ToString()) - 1];
                if (temp == "i,j")
                    temp = "i";
                to_return += temp;
            }

            return to_return;
        }

        public string poziomo_przejsciowa_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);

            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += poziomo_przejsciowa(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }

        public string poziomo_szyfruj_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);
            for(int i = 0; i< tok.Length; i++)
                if(tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += poziomo_szyforwanie(tok[i]);
                else
                    to_return += tok[i];


            return to_return;
        }

        public string poziomo_rozszyfruj_specjlane(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);
            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += poziomo_rozszyfruj(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }
        #endregion

        #region gora_dol
        public string gora_dol_przejsciowa(string text)
        {
            string to_return = "";
            List<Tuple<int, int>> list = new List<Tuple<int, int>>();
            for (int i = 0; i < text.Length; i++)
            {
                list.Add(find(text[i].ToString()));
            }
            Tuple<int, int>[] arr = list.ToArray();
            for (int i = 0; i < arr.Length - 1; i++)
            {
                to_return += arr[i].Item1.ToString() + arr[i + 1].Item2.ToString();
            }
            to_return += arr[0].Item2.ToString() + arr[arr.Length - 1].Item1.ToString();
            return to_return;
        }
        public string gora_dol_szyforwanie(string text)
        {
            string tmp = gora_dol_przejsciowa(text);
            string to_return = "";

            for (int i = 0; i < tmp.Length; i += 2)
            {
                string temp = matrix[Int32.Parse(tmp[i + 1].ToString()) - 1, Int32.Parse(tmp[i].ToString()) - 1];
                if (temp == "i,j")
                    temp = "i";
                to_return += temp;
            }

            return to_return;
        }
        public string gora_dol_przejsciowa_rozszyforwanie(string text)
        {
            string to_return = "";

            for (int i = 0; i < text.Length; i++)
            {
                Tuple<int, int> tmp = find(text[i].ToString());
                to_return += tmp.Item1.ToString() + tmp.Item2.ToString();
            }

            return to_return;
        }
        public string gora_dol_rozszyforwanie(string text)
        {
            string to_return = "";
            string tmp = gora_dol_przejsciowa_rozszyforwanie(text);

            string left = "";
            for (int i = 0; i < tmp.Length; i += 2)
                left += tmp[i];

            string right = tmp[tmp.Length - 1].ToString();
            for (int i = 1; i < tmp.Length - 1; i += 2)
                right += tmp[i];

            char left_last = left[left.Length - 1];
            char right_first = right[0];

            var s_left = new StringBuilder(left);
            var s_right = new StringBuilder(right);

            s_left[s_left.Length - 1] = right_first;
            s_right[0] = left_last;

            string left_fin = s_left.ToString();
            string right_fin = s_right.ToString();

            string fin = right_fin + right;

            for (int i = 0; i < left.Length; i++)
            {
                string temp = matrix[Int32.Parse(right_fin[i].ToString()) - 1, Int32.Parse(left_fin[i].ToString()) - 1];
                if (temp == "i,j")
                    temp = "i";
                to_return += temp;
            }

            return to_return;
        }
        public string gora_dol_przejsciowa_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);

            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += gora_dol_przejsciowa(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }
        public string gora_dol_szyfruj_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);
            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += gora_dol_szyforwanie(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }
        public string gora_dol_rozszyfruj_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);
            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += gora_dol_rozszyforwanie(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }

        #endregion

        #region dol_gora
        public string dol_gora_przejsciowa(string text)
        {
            string to_return = "";
            List<Tuple<int, int>> list = new List<Tuple<int, int>>();
            for (int i = 0; i < text.Length; i++)
            {
                list.Add(find(text[i].ToString()));
            }
            Tuple<int, int>[] arr = list.ToArray();
            for (int i = 0; i < arr.Length - 1; i++)
            {
                to_return += arr[i].Item2.ToString() + arr[i + 1].Item1.ToString();
            }
            to_return += arr[0].Item1.ToString() + arr[arr.Length - 1].Item2.ToString();
            return to_return;
        }

        public string dol_gora_szyfrowanie(string text)
        {
            string tmp = dol_gora_przejsciowa(text);
            string to_return = "";

            for (int i = 0; i < tmp.Length; i += 2)
            {
                string temp = matrix[Int32.Parse(tmp[i + 1].ToString()) - 1, Int32.Parse(tmp[i].ToString()) - 1];
                if (temp == "i,j")
                    temp = "i";
                to_return += temp;
            }

            return to_return;
        }

        public string dol_gora_przejsciowa_rozszyfrowanie(string text)
        {
            string to_return = "";

            for (int i = 0; i < text.Length; i++)
            {
                Tuple<int, int> tmp = find(text[i].ToString());
                to_return += tmp.Item1.ToString() + tmp.Item2.ToString();
            }

            return to_return;
        }

        public string dol_gora_rozszyforwanie(string text)
        {
            string to_return = "";
            string tmp = dol_gora_przejsciowa_rozszyfrowanie(text);

            string prawa = "";
            for(int i = 0; i<tmp.Length - 1; i+=2)
            {
                prawa += tmp[i];
            }

            string lewa = tmp[tmp.Length-1].ToString();
            for(int i= 1; i< tmp.Length-1; i+=2)
            {
                lewa += tmp[i];
            }

            string fin = lewa + prawa;
            var s_fin = new StringBuilder(fin);
            char first = s_fin[0];
            char s_last = s_fin[s_fin.Length - 1];
            s_fin[0] = s_last;
            s_fin[s_fin.Length - 1] = first;

            string tmp_ = s_fin.ToString();

            string left = tmp_.Substring(0, tmp_.Length / 2);
            string right = tmp_.Substring(tmp_.Length / 2, tmp_.Length - tmp_.Length / 2);

            for (int i = 0; i < left.Length ; i++)
            {
                string temp = matrix[Int32.Parse(right[i].ToString()) - 1, Int32.Parse(left[i].ToString()) - 1];
                if (temp == "i,j")
                    temp = "i";
                to_return += temp;
            }

            return to_return;
        }

        public string dol_gora_przejsciowa_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);

            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += dol_gora_przejsciowa(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }

        public string dol_gora_szyfruj_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);
            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += dol_gora_szyfrowanie(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }

        public string dol_gora_rozszyfruj_specjalne(string text)
        {
            string to_return = "";
            string[] tok = tokens_raw(text);

            for (int i = 0; i < tok.Length; i++)
                if (tok[i][0] >= 'a' && tok[i][0] <= 'z')
                    to_return += dol_gora_rozszyforwanie(tok[i]);
                else
                    to_return += tok[i];

            return to_return;
        }
        #endregion
    }
}
