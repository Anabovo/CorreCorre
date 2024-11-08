namespace CorreCorre

public class Animacao
{
    protected List <String> animacao1 =new List <String> ();
    protected List <String> animacao2 =new List <String> ();
    protected List <String> animacao3 =new List <String> ();

    protected bool loop = true;
    protected int animacaoAtiva = 1;

    bool parado=true;
    int frameAtual=1;

    protected Image compImagem;


    public Animacao(Image a)
    {
        compImagem=a;
    }

    public void Stop()
	{
		parado = true;
	}

	public void Play()
	{
		parado=false;
	}

	public void SetAnimacaoAtiva(int a)
	{
		animacaoAtiva=a;
	}

    public void Desenha()
    {
        if (parado)
           return;

        String  nomeArquivo="";
        int tamanhoAnimacao=0;

        if (animacaoAtiva ==1)
        {
            nomeArquivo = animacao1[frameAtual];
            tamanhoAnimacao = animacao1.Count;
        }
        else if (animacaoAtiva ==2)
        {
            nomeArquivo = animacao2[frameAtual];
            tamanhoAnimacao = animacao2.Count;
        }
        else if (animacaoAtiva ==3)
        {
            nomeArquivo = animacao3[frameAtual];
            tamanhoAnimacao = animacao3.Count;
        }
    }

}