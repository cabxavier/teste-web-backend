using System.Text.RegularExpressions;

namespace Util
{
    public static class Utils
    {
        #region Método ValidarPorExpressaoRegular
        public static bool ValidarPorExpressaoRegular(string Value, string Expressao)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return false;
            }

            return Regex.IsMatch(Value, Expressao);
        }
        #endregion


        #region Método OnlyNumber
        public static string OnlyNumber(string In)
        {
            string _Out = "";

            if (!string.IsNullOrWhiteSpace(In))
            {
                foreach (char c in In.ToCharArray())
                {
                    if (char.IsNumber(c))
                    {
                        _Out += c.ToString();
                    }
                }
            }

            return _Out;
        }
        #endregion


        #region Método FormatarCep
        public static string FormatarCep(string Value)
        {
            string _CepFormatado = "";

            if (!string.IsNullOrWhiteSpace(Value))
            {
                _CepFormatado = OnlyNumber(Value);

                if (Value.Length == 8)
                {
                    _CepFormatado = string.Format("{0:00000-000}", double.Parse(_CepFormatado));
                }
            }

            return _CepFormatado;
        }
        #endregion


        #region Método FormatCnpj
        public static string FormatCnpj(string Cnpj)
        {
            string _Out = "";

            if (!string.IsNullOrWhiteSpace(Cnpj))
            {
                string sAux = OnlyNumber(Cnpj);

                _Out = FormatCnpj(int.Parse(sAux));
            }

            return _Out;
        }
        #endregion


        #region Método FormatCnpj
        public static string FormatCnpj(int Cnpj)
        {
            return string.Format(@"{0:00\.000\.000\/0000\-00}", Cnpj);
        }
        #endregion


        #region Método FormatCpf
        public static string FormatCpf(string Cpf)
        {
            string _Out = "";

            if (!string.IsNullOrWhiteSpace(Cpf))
            {
                _Out = OnlyNumber(Cpf);

                _Out = FormatCpf(int.Parse(_Out));
            }

            return _Out;
        }
        #endregion


        #region Método FormatCpf
        public static string FormatCpf(int Cpf)
        {
            return string.Format(@"{0:000\.000\.000\-00}", Cpf);
        }
        #endregion


        #region Método FormatCpfOrCnpj
        public static string FormatCpfOrCnpj(string Documento)
        {
            string _Out = "";

            if (!string.IsNullOrWhiteSpace(Documento))
            {
                string _sAux = OnlyNumber(Documento);

                if (_sAux.Length == 14)
                {
                    _Out = FormatCnpj(_sAux);
                }
                else
                {
                    if (_sAux.Length == 11)
                    {
                        _Out = FormatCpf(_sAux);
                    }
                }

            }

            return _Out;
        }
        #endregion


        #region Método FormatarTelefone
        public static string FormatarTelefone(string Telefone)
        {
            string _Out = "";

            if (!string.IsNullOrWhiteSpace(Telefone))
            {
                string tel = OnlyNumber(Telefone);

                if (tel.Length == 8)
                {
                    _Out = string.Format("{0}-{1}", tel.Substring(0, 4), tel.Substring(4, 4));
                }
                else
                {
                    if (tel.Length == 10)
                    {
                        _Out = string.Format("({0}) {1}-{2}", tel.Substring(0, 2), tel.Substring(2, 4), tel.Substring(6, 4));
                    }
                }
            }

            return _Out;
        }
        #endregion


        #region Método ValidarCnpj
        public static bool ValidarCnpj(string Cnpj)
        {
            Cnpj = OnlyNumber(Cnpj);

            if (Cnpj.Length != 14)
            {
                return false;
            }
            else
            {
                int[] _ValoresPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] _ValoresSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int _PrimeiroDigito = 0;
                int _SegundoDigito = 0;
                int _DigitoAtualCnpj;

                for (int i = 0; i <= 11; i++)
                {
                    _DigitoAtualCnpj = int.Parse(Cnpj.Substring(i, 1));
                    _PrimeiroDigito += (_DigitoAtualCnpj * _ValoresPrimeiroDigito[i]);
                    _SegundoDigito += (_DigitoAtualCnpj * _ValoresSegundoDigito[i]);
                }

                _PrimeiroDigito = _PrimeiroDigito % 11;

                if (_PrimeiroDigito < 2)
                {
                    _PrimeiroDigito = 0;
                }
                else
                {
                    _PrimeiroDigito = 11 - _PrimeiroDigito;
                }

                if (_PrimeiroDigito != (int.Parse(Cnpj.Substring(12, 1))))
                {
                    return false;
                }
                else
                {
                    _SegundoDigito += (_PrimeiroDigito * _ValoresSegundoDigito[12]);
                    _SegundoDigito = _SegundoDigito % 11;
                    if (_SegundoDigito < 2)
                    {
                        _SegundoDigito = 0;
                    }
                    else
                    {
                        _SegundoDigito = 11 - _SegundoDigito;
                    }

                    return _SegundoDigito == (int.Parse(Cnpj.Substring(13, 1)));
                }
            }
        }
        #endregion


        #region Método ValidarCpf
        public static bool ValidarCpf(string Cpf)
        {
            Cpf = OnlyNumber(Cpf);

            if (Cpf.Length != 11)
            {
                return false;
            }
            else
            {
                int[] _ValoresPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] _ValoresSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int _PrimeiroDigito = 0;
                int _SegundoDigito = 0;
                int _DigitoAtualCpf;

                for (int i = 0; i <= 8; i++)
                {
                    _DigitoAtualCpf = int.Parse(Cpf.Substring(i, 1));
                    _PrimeiroDigito += (_DigitoAtualCpf * _ValoresPrimeiroDigito[i]);
                    _SegundoDigito += (_DigitoAtualCpf * _ValoresSegundoDigito[i]);
                }

                _PrimeiroDigito = _PrimeiroDigito % 11;

                if (_PrimeiroDigito < 2)
                {
                    _PrimeiroDigito = 0;
                }
                else
                {
                    _PrimeiroDigito = 11 - _PrimeiroDigito;
                }

                if (_PrimeiroDigito != int.Parse(Cpf.Substring(9, 1)))
                {
                    return false;
                }

                _SegundoDigito += (_PrimeiroDigito * _ValoresSegundoDigito[9]);

                _SegundoDigito = _SegundoDigito % 11;

                if (_SegundoDigito < 2)
                {
                    _SegundoDigito = 0;
                }
                else
                {
                    _SegundoDigito = 11 - _SegundoDigito;
                }

                return _SegundoDigito == (int.Parse(Cpf.Substring(10, 1)));
            }
        }
        #endregion
    }
}
