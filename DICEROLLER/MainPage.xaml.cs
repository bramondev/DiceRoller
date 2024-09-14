namespace DiceRoller
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            SidesPicker.SelectedIndex = 0; // Seleciona um valor padrão no Picker de lados
        }

        private void BtnSpin_Clicked(object sender, EventArgs e)
        {
            // Verifica se a quantidade de lados e dados foram selecionados/inseridos
            if (SidesPicker.SelectedItem == null || string.IsNullOrWhiteSpace(DiceCountEntry.Text))
            {
                ResultLabel.Text = "Selecione os lados e insira a quantidade de dados.";
                return;
            }

            // Pega o valor dos lados e a quantidade de dados
            int lados = Convert.ToInt32(SidesPicker.SelectedItem);
            int quantidadeDados;

            if (!int.TryParse(DiceCountEntry.Text, out quantidadeDados) || quantidadeDados <= 0)
            {
                ResultLabel.Text = "Insira uma quantidade válida de dados.";
                return;
            }

            // Pega o modificador selecionado no Picker
            int modificador = 0;
            if (ModifierPicker.SelectedItem != null)
            {
                modificador = Convert.ToInt32(ModifierPicker.SelectedItem.ToString().Replace("+", ""));
            }

            Random random = new Random();
            int somaResultados = 0;
            string resultadosIndividuais = "";

            // Rola os dados e adiciona o modificador
            for (int i = 0; i < quantidadeDados; i++)
            {
                int valorSorteado = random.Next(1, lados + 1);
                int valorFinal = valorSorteado + modificador;
                somaResultados += valorFinal;
                resultadosIndividuais += $"{valorSorteado} (+{modificador}) = {valorFinal}" + (i == quantidadeDados - 1 ? "" : ", ");
            }

            // Exibe o resultado total e os valores de cada dado rolado com o modificador
            ResultLabel.Text = $"Soma: {somaResultados} (Valores: {resultadosIndividuais})";
        }

        // Método chamado ao clicar no botão "Sobre os Programadores"
        private void OnAboutClicked(object sender, EventArgs e)
        {
            // Abrir os dois perfis do GitHub
            try
            {
                Launcher.Default.OpenAsync(new Uri("https://github.com/TRIBUNAA"));
                Launcher.Default.OpenAsync(new Uri("https://github.com/bramondev"));
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", $"Não foi possível abrir os links: {ex.Message}", "OK");
            }
        }
    }
}
