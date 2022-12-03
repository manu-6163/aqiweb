using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCDCRUD.Models;

public partial class Asegurado
{
    [Required(ErrorMessage = "Debe ingresar un número válido.")]
    public int Identificacion { get; set; }

    [Required(ErrorMessage = "Primer nombre es requerido.")]
    [MaxLength(50)]
    public string Nombre1 { get; set; } = null!;

    [MaxLength(50)]
    public string? Nombre2 { get; set; }

    [Required(ErrorMessage = "Primer apellido es requerido.")]
    [MaxLength(50)]
    public string Apellido1 { get; set; } = null!;

    [Required(ErrorMessage = "Segundo apellido es requerido.")]
    [MaxLength(50)]
    public string Apellido2 { get; set; } = null!;

    [Required(ErrorMessage = "Número de contacto es requerido.")]
    public string Celular { get; set; } = null!;

    [Required(ErrorMessage = "Email es requerido.")]
    [DataType(DataType.EmailAddress)]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Fecha de nacimiento es requerida.")]
    public DateTime FechaNacimiento { get; set; }

    [DataType(DataType.Currency)]
    [Required(ErrorMessage = "Valor seguro es requerido.")]
    [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
    [Precision(10, 2)]
    public decimal? ValorSeguro { get; set; }
}
