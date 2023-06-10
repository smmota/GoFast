﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GoFast.API.Models;
using GoFast.API.Models.ViewModels;

public class MotoristaViewModel
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Nascimento { get; set; }

    [ForeignKey("Endereco")]
    public Guid IdEnderecoRefMotorista { get; set; }

    [ForeignKey("Carro")]
    public Guid IdCarroRefMotorista { get; set; }

    public EnderecoViewModel Endereco { get; set; }

    public CarroViewModel Carro { get; set; }
}
