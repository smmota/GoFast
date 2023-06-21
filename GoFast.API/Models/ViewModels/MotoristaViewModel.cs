using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoFast.API.Models;
using GoFast.API.Models.ViewModels;

public class MotoristaViewModel
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public DateTime Nascimento { get; set; }

    public Guid IdEnderecoRefMotorista { get; set; }

    public Guid IdCarroRefMotorista { get; set; }

    public EnderecoViewModel Endereco { get; set; }

    public CarroViewModel Carro { get; set; }
}
