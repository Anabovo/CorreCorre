using FFImageLoading.Maui;

namespace CorreCorre;

public partial class MainPage : ContentPage
{
	bool estaMorto=false;
	bool estaPulando=false;
	const int tempoEntreFrames=25;
	int velocidade1=0;
	int velocidade2 =0;
	int velocidade3=0;
	int velocidade=0;
	int larguraJanela=0;
	int alturajanela0;
	Player player;

	const int forcaGravidade=6;
	bool estaNoChao=true;
	bool estaNoar=false;
	int tempoPulando=0;
	int tempoNoAr=0;
	const int forcaPulo=8;
	const int maxTempoPulando=6;
	const int maxTempoNoAr=4;

	public MainPage()
	{
		InitializeComponent();
		player=new Player(imgPlayer);
		player.Run();
	}

    protected override void OnSizeAllocated(double w, double h)
    {
        base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
    }

	void CalculaVelocidade(double w)
	{
		velocidade1=(int)(w*0.001);
		velocidade2=(int)(w*0.004);
		velocidade3=(int)(w*0.008);
		velocidade=(int)(w*0.01);
	}
	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var arvores in HSLayerArv1.Children)
			(arvores as Image). WidthRequest = w;
		foreach (var Arvores in HSLayerArv2.Children)
			(Arvores as Image). WidthRequest = w;
		foreach (var chao in HSLayerChao.Children)
			(chao as Image). WidthRequest = w;

		HSLayerArv1.WidthRequest = w;
		HSLayerArv2.WidthRequest = w;
		HSLayerChao.WidthRequest = w;
	}
	async void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(HSLayerArv1);
		GerenciaCenario(HSLayerArv2);
		GerenciaCenario(HSLayerChao);
		player.Desenha();
		await Task.Delay(tempoEntreFrames);
	}
	void MoveCenario()
	{
		HSLayerArv1.TranslationX-=velocidade1;
		HSLayerArv2.TranslationX-=velocidade2;
		HSLayerChao.TranslationX-=velocidade3;
	}
	void GerenciaCenario(HorizontalStackLayout hsl)
	{
		var view = (hsl.Children.First() as Image);
		if (view.WidthRequest + hsl.TranslationX < 0)
		{
			hsl.Children.Remove(view);
			hsl.Children.Add(view);
			hsl.TranslationX=view.TranslationX;
		}
	}
	async Task Desenha()
	{
		while(!estaMorto)
		{
			GerenciaCenarios();
			
			if (!estaPulando && !estaNoar)
			{
				AplicaGravidae();
				player.Desenha();
			}
			else
			  AplicaPulo();
			
			await Task.Delay(tempoEntreFrames);
		}
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenha();
    }

	void AplicaGravidae()
	{
		if(player.GetY()<0)
		   layer.MoveY(forcaGravidade);
		else if (player.GetY()>=0)
		{
			player.SetY(0);
			estaNoChao=true;
		}
	}

	void AplicaPulo()
	{
		estaNoChao=false;
		if(estaPulando && tempoPulando >= maxTempoPulando)
		{
			estaPulando=false;
			estaNoar=true;
			tempoNoAr=0;
		}
		else if (estaNoar && tempoNoAr >= maxTempoPulando)
		{
			estaPulando=false;
			estaNoar=true;
			tempoNoAr=0;
			tempoPulando=0;
		}
		else if (estaPulando && tempoPulando < maxTempoPulando)
		{
			player.MoveY(-forcaPulo);
			tempoPulando++;
		}
		else if(estaNoar)
		tempoNoAr++;
	}

	void onGridtapped(Object o, TappedEventArgs a)
	{
		if(estaNoChao)
		estaPulando=true;
	}

}


