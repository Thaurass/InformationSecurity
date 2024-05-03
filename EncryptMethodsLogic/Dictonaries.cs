namespace EncryptMethodsLogic;

public class Dictonaries
{
    public Dictionary<char, double> letterProbability = new();

    public Dictionary<char, double> Probability = new();
    public Dictionary<char, double> RusProbability = new();
    public Dictionary<char, double> EngProbability = new();

    public string AllLeters = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
    public string RUSAllLeters = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
    public string ENGAllLeters = "abcdefghijklmnopqrstuvwxyz";

    public void RusProbilities()
    {
        Probability = RusProbability;

        RusProbability.Add('а', 8.04);
        RusProbability.Add('б', 1.55);
        RusProbability.Add('в', 4.75);
        RusProbability.Add('г', 1.88);
        RusProbability.Add('д', 2.95);
        RusProbability.Add('е', 8.21);
        RusProbability.Add('ж', 0.88);
        RusProbability.Add('з', 1.61);
        RusProbability.Add('и', 7.98);
        RusProbability.Add('й', 1.36);
        RusProbability.Add('к', 3.49);
        RusProbability.Add('л', 4.32);
        RusProbability.Add('м', 3.11);
        RusProbability.Add('н', 6.72);
        RusProbability.Add('о', 10.61);
        RusProbability.Add('п', 2.82);
        RusProbability.Add('р', 5.38);
        RusProbability.Add('с', 5.71);
        RusProbability.Add('т', 5.83);
        RusProbability.Add('у', 2.28);
        RusProbability.Add('ф', 0.41);
        RusProbability.Add('х', 1.02);
        RusProbability.Add('ц', 0.58);
        RusProbability.Add('ч', 1.23);
        RusProbability.Add('ш', 0.55);
        RusProbability.Add('щ', 0.34);
        RusProbability.Add('ъ', 0.03);
        RusProbability.Add('ы', 1.91);
        RusProbability.Add('ь', 1.39);
        RusProbability.Add('э', 0.31);
        RusProbability.Add('ю', 0.63);
        RusProbability.Add('я', 2.00);


    }

    public void EngProbilities()
    {
        EngProbability.Add('a', 8.55);
        EngProbability.Add('b', 1.60);
        EngProbability.Add('c', 3.16);
        EngProbability.Add('d', 3.87);
        EngProbability.Add('e', 12.10);
        EngProbability.Add('f', 2.18);
        EngProbability.Add('g', 2.09);
        EngProbability.Add('h', 4.96);
        EngProbability.Add('i', 7.33);
        EngProbability.Add('j', 0.22);
        EngProbability.Add('k', 0.81);
        EngProbability.Add('l', 4.21);
        EngProbability.Add('m', 2.53);
        EngProbability.Add('n', 7.17);
        EngProbability.Add('o', 7.47);
        EngProbability.Add('p', 2.07);
        EngProbability.Add('q', 0.10);
        EngProbability.Add('r', 6.33);
        EngProbability.Add('s', 6.73);
        EngProbability.Add('t', 8.94);
        EngProbability.Add('u', 2.68);
        EngProbability.Add('v', 1.06);
        EngProbability.Add('w', 1.83);
        EngProbability.Add('x', 0.19);
        EngProbability.Add('y', 1.72);
        EngProbability.Add('z', 0.11);
    }
}
