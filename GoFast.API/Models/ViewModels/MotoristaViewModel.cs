using System.ComponentModel.DataAnnotations;
using GoFast.API.Models;
using GoFast.API.Models.ViewModels;

public class MotoristaViewModel
{
    [Required]
    [MaxLength(150)]
    public string Nome { get; set; }
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    [Required]
    public DateTime Nascimento { get; set; }
    [Required]
    public EnderecoViewModel Endereco { get; set; }
    [Required]
    public CarroViewModel Carro { get; set; }

    public Motorista MapTo()
    {
        var enderecoDM = new Endereco(new Guid(), Endereco.Rua, Endereco.Numero, Endereco.CEP, Endereco.Bairro, Endereco.Cidade, Endereco.Estado, Endereco.Complemento);
        var documentoDM = new DocumentoCarro(new Guid(), Carro.DocumentoCarro.TipoDocumento, Carro.DocumentoCarro.Numero, Carro.DocumentoCarro.Expedicao, Carro.DocumentoCarro.IdBlob, Carro.DocumentoCarro.Renovacao);
        var carroDM = new Carro(new Guid(), Carro.Placa, Carro.Modelo, Carro.AnoFabricacao, documentoDM);

        return new Motorista(new Guid(), Nome, Email, Nascimento);
    }
}
