using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class TextUtil
    {

        #region Método OnlyNumber
        public static string OnlyNumber(string In)
        {
            string _Out = "";

            if (In == null)
            {
                return "";
            }

            foreach (char c in In.ToCharArray())
            {
                if (char.IsNumber(c))
                    _Out += c.ToString();
            }

            return _Out;
        }
        #endregion


        #region FormatCep
        public static string FormatCep(string Cep)
        {
            if (string.IsNullOrEmpty(Cep))
            {
                return null;
            }

            Cep = OnlyNumber(Cep);

            if (Cep.Length == 8)
            {
                Cep = string.Format("{0:00000-000}", int.Parse(Cep));
            }

            return Cep;
        }
        #endregion


        #region FormatCnpj
        public static string FormatCnpj(string Cnpj)
        {
            if (string.IsNullOrEmpty(Cnpj))
            {
                return null;
            }

            Cnpj = OnlyNumber(Cnpj);

            if (Cnpj.Length == 14)
            {
                Cnpj = string.Format(@"{0:00\.000\.000\/0000\-00}", int.Parse(Cnpj));
            }

            return Cnpj;
        }
        #endregion


        #region FormatCpf
        public static string FormatCpf(string Cpf)
        {
            if (string.IsNullOrEmpty(Cpf))
            {
                return null;
            }

            Cpf = OnlyNumber(Cpf);

            if (Cpf.Length == 11)
            {
                Cpf = string.Format(@"{0:000\.000\.000\-00}", int.Parse(Cpf));
            }

            return Cpf;
        }
        #endregion


        #region FormatCnpjOrCpf
        public static string FormatCnpjOrCpf(string CnpjOrCpf)
        {
            if (string.IsNullOrEmpty(CnpjOrCpf))
            {
                return null;
            }

            CnpjOrCpf = OnlyNumber(CnpjOrCpf);

            if (CnpjOrCpf.Length == 14)
            {
                CnpjOrCpf = FormatCnpj(CnpjOrCpf);
            }

            return CnpjOrCpf;
        }
        #endregion


        #region FormatTelefone
        public static string FormatTelefone(string DDD, string Prefixo, string Sufixo)
        {
            string _Out = "";

            DDD = OnlyNumber(DDD);
            Prefixo = OnlyNumber(Prefixo);
            Sufixo = OnlyNumber(Sufixo);

            if (DDD.Length == 2)
            {
                if ((Prefixo.Length == 4) || (Prefixo.Length == 5))
                {
                    if (Sufixo.Length == 4)
                    {
                        _Out = string.Format("({0}) {1}-{2}", DDD, Prefixo, Sufixo);
                    }
                }
            }

            return _Out;
        }
        #endregion


        #region IsValidCnpj
        public static bool IsValidCnpj(string Cnpj)
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


        #region IsValidCpf
        public static bool IsValidCpf(string Cpf)
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
