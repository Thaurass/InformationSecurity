using CommunityToolkit.Maui.Alerts;
using EncryptMethodsLogic;

namespace InformationSecurity;

public partial class EuclideanAlgorithmInterface : ContentPage
{
	public EuclideanAlgorithmInterface()
	{
		InitializeComponent();
        _dictonaries.RusProbilities();
        _dictonaries.EngProbilities();
    }

    EuclideanAlgorithmLogic _euclideanAlgorithmLogic = new();
    CaesarCipherLogic _caesarCipherLogic = new();
    Dictonaries _dictonaries = new();


    public void Encrypt_Click(object sender, EventArgs args)
    {
        Data.Text = string.Empty;

        bool b1 = Check_number(Method.Text, 1, 3);
        bool b2 = Check_number(FirstNum.Text, 1, int.MaxValue);
        bool b3 = Check_number(SecondNum.Text, 1, int.MaxValue);
        if (FirstNum.Text == null && SecondNum.Text == null) 
        {
            Data.Text = _euclideanAlgorithmLogic.Time_Click();
        }
        if (b1 && b2 && b3)
        {
            Data.Text = _euclideanAlgorithmLogic.Encrypt_func_Euclidean_Algorithm_4(FirstNum.Text, SecondNum.Text, Method.Text);
        }
    }


    private async void SaveButton_Click(object sender, EventArgs args)
    {
        await _caesarCipherLogic.SaveCipherData(FirstNum.Text + "|" + SecondNum.Text);
        if (_caesarCipherLogic.IsSaveSuccessful)
        {
            await Toast.Make($"File is saved").Show();
        }
        else
        {
            await Toast.Make($"File is not saved").Show();
        }
    }

    public async void LoadFile(object sender, EventArgs args)
    {
        await _caesarCipherLogic.GetCipherData();
        if (_caesarCipherLogic.FileData[0] != "first_line")
        {
            FirstNum.Text = _caesarCipherLogic.FileData[0];
            SecondNum.Text = _caesarCipherLogic.FileData[1];
        }

    }

    public bool Check_empty(Entry sender)
    {
        bool err = true;
        if (sender.Text == string.Empty) { err = false; }
        return err;

    }

    private bool Check_number(string sender, int lover, int up)
    {
        bool err = false;
        int step;
        if (int.TryParse(sender, out step))
        {
            if (step >= lover && step <= up) { err = true; }
        }
        return err;

    }

    private void ClearEntry_Click(object sender, EventArgs args)
    {
        FirstNum.Text = string.Empty;
        SecondNum.Text = string.Empty;
        Method.Text = string.Empty;
        Data.Text = string.Empty;
    }
}