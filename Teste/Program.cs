// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using GoFast.API.Data;
using GoFast.API.Data.Repositories;

SqlContext context = null;
UsuarioRepository repository = new UsuarioRepository(context);

var usuario = repository.GetUsuarioByUserAndPassword("smota", "123456");