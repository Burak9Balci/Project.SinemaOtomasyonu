using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class DanteCrypto
{
    /// <summary>
    ///  sifreyi cryptolar
    /// </summary>
    /// <param name="hayat"></param>
    /// <returns></returns>
    public static string CrypHeaven(string hayat)
    {
     
        Random dnt = new Random();
        string[] sinArray = { "limbo", "lust", "gluttony", "greed", "anger", "heresy", "violence", "fraud", "treachery" };
        string beden = "";
        foreach (char gunah in hayat)
        {
            int gunahSayisi;
            switch (dnt.Next(1, 10))
            {
                case 1:
                    gunahSayisi = (Convert.ToInt32(gunah) + 1) * 3;
                    beden += $"{gunahSayisi.ToString()}{sinArray[0]}";
                    break;

                case 2:
                    gunahSayisi = (Convert.ToInt32(gunah) + 2) * 2;
                    beden += $"{gunahSayisi.ToString()}{sinArray[1]}";
                    break;

                case 3:
                    gunahSayisi = (Convert.ToInt32(gunah) + 3) * 1;
                    beden += $"{gunahSayisi.ToString()}{sinArray[2]}";
                    break;

                case 4:
                    gunahSayisi = (Convert.ToInt32(gunah) + 4) * 3;
                    beden += $"{gunahSayisi.ToString()}{sinArray[3]}";
                    break;

                case 5:
                    gunahSayisi = (Convert.ToInt32(gunah) + 5) * 2;
                    beden += $"{gunahSayisi.ToString()}{sinArray[4]}";
                    break;

                case 6:
                    gunahSayisi = (Convert.ToInt32(gunah) + 6) * 1;
                    beden += $"{gunahSayisi.ToString()}{sinArray[5]}";
                    break;

                case 7:
                    gunahSayisi = (Convert.ToInt32(gunah) + 7) * 3;
                    beden += $"{gunahSayisi.ToString()}{sinArray[6]}";
                    break;

                case 8:
                    gunahSayisi = (Convert.ToInt32(gunah) + 8) * 2;
                    beden += $"{gunahSayisi.ToString()}{sinArray[7]}";
                    break;

                case 9:
                    gunahSayisi = (Convert.ToInt32(gunah) + 9) * 1;
                    beden += $"{gunahSayisi.ToString()}{sinArray[8]}";
                    break;

            }

        }

        return beden;
    }
    
    /// <summary>
    /// Criptolu sifreyi Normal hale ceker
    /// </summary>
    /// <param name="beden"></param>
    /// <returns></returns>
    public static string CrypHell(string beden)
    {
        //Kullanma Kılvauzu
        //beden Kriptoyu kaldirmek istediğimiz deger
        //ruh criptonun temizlenmiş hali
        //arinmis criptonun 1 adımı denk gelen gunahın ilk ayrılma anı
        //Aydinlanma Miktari 2. adımı Gunahsayisından arındırıyoruz
        //ardından aydinlanma miktarini chara dönüştürüp ruhun bir parçasını elde ediyoruz
        //Ve son adım olarak ruhParçalarını döngüde birleştiriyoruz
        string ruh = "";
        List<string> parts = Regex.Split(beden, @"(?<=[limbolustgluttonygreedangerheresyviolencefraudtreachery])").ToList();
        foreach (string item in parts)
        {
            if (item.Contains("limbo"))
            {

                string arinmis = item.TrimEnd("limbo".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 3) - 1;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("lust"))
            {

                string arinmis = item.TrimEnd("lust".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 2) - 2;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("gluttony"))
            {

                string arinmis = item.TrimEnd("gluttony".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 1) - 3;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("greed"))
            {

                string arinmis = item.TrimEnd("greed".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 3) - 4;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("anger"))
            {

                string arinmis = item.TrimEnd("anger".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 2) - 5;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("heresy"))
            {

                string arinmis = item.TrimEnd("heresy".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 1) - 6;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("violence"))
            {

                string arinmis = item.TrimEnd("violence".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 3) - 7;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("fraud"))
            {

                string arinmis = item.TrimEnd("fraud".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 2) - 8;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
            else if (item.Contains("treachery"))
            {

                string arinmis = item.TrimEnd("treachery".ToCharArray());

                int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 1) - 9;
                string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                ruh += ruhParcasi;
            }
        }
        return ruh;
    }

    
    public static string Crypt(string a)
    {
        Random rnd = new Random();
        char[] charArray = { 'B', 'U', 'R', 'A', 'K'};
        string hashedCode = "";
        foreach (char item in a)
        {
            int tempInteger;
            switch (rnd.Next(1, 4))
            {
                case 1:
                    tempInteger = (Convert.ToInt32(item) + 1) * 2;
                    hashedCode += $"{tempInteger.ToString()}{charArray[0]}";
                    break;
                case 2:
                    tempInteger = (Convert.ToInt32(item) + 2) * 3;
                    hashedCode += $"{tempInteger.ToString()}{charArray[1]}";
                    break;
                case 3:
                    tempInteger = (Convert.ToInt32(item) + 3) * 4;
                    hashedCode += $"{tempInteger.ToString()}{charArray[2]}";
                    break;
                case 4:
                    tempInteger = (Convert.ToInt32(item) + 4) * 5;
                    hashedCode += $"{tempInteger.ToString()}{charArray[3]}";
                    break;
                case 5:
                    tempInteger = (Convert.ToInt32(item) + 5) * 6;
                    hashedCode += $"{tempInteger.ToString()}{charArray[4]}";
                    break;
            }

        }

        return hashedCode;
    }

    public static string DeCrypt(string a)
    {
        string decryptedCode = "";
        List<string> parts = Regex.Split(a, @"(?<=[BURAK])").ToList(); 
        foreach (string item in parts)
        {
            if (item.Contains("B"))
            {
                string element = item.TrimEnd('B');
                int asciiCode = (Convert.ToInt32(element) / 2) - 1;
                string character = Convert.ToChar(asciiCode).ToString();
                decryptedCode += character;
            }
            else if (item.Contains("U"))
            {
                string element2 = item.TrimEnd('U');
                int asciiCode2 = (Convert.ToInt32(element2) / 3) - 2;
                string character2 = Convert.ToChar(asciiCode2).ToString();
                decryptedCode += character2;
            }
            else if (item.Contains("R"))
            {

                string element3 = item.TrimEnd('R');
                int asciiCode3 = (Convert.ToInt32(element3) / 4) - 3;
                string character3 = Convert.ToChar(asciiCode3).ToString();
                decryptedCode += character3;
            }
            else if (item.Contains("A"))
            {

                string element3 = item.TrimEnd('A');
                int asciiCode3 = (Convert.ToInt32(element3) / 5) - 4;
                string character3 = Convert.ToChar(asciiCode3).ToString();
                decryptedCode += character3;
            }
            else if (item.Contains("K"))
            {

                string element3 = item.TrimEnd('K');
                int asciiCode3 = (Convert.ToInt32(element3) / 6) - 5;
                string character3 = Convert.ToChar(asciiCode3).ToString();
                decryptedCode += character3;
            }
        }
        return decryptedCode;



    }

    public static string Sifrele(string naNormal)
    {
        byte[] dizi = ASCIIEncoding.ASCII.GetBytes(naNormal);
        naNormal = Convert.ToBase64String(dizi);
        return naNormal;
    }
    public static string SifreBoz(string boz)
    {
        byte[] dizi = Convert.FromBase64String(boz);
        boz = ASCIIEncoding.ASCII.GetString(dizi);
        return boz;
    }
}
