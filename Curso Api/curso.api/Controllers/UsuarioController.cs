using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;        
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(IUsuarioRepository usuarioRepository                               
                                , IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Este serviço permite autenticar um usuario cadastrado
        /// </summary>
        /// <param name="loginViewModelInput">View da Model de Login</param>
        /// <returns>Retornar status ok, login e senha ok</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput), Description ="Sucesso ao autenticar 2" )]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput), Description = "Campos Obrigatorios 2")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel), Description = "Erro Interno 2")]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);
            if (usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar. (Usuário não encontrado ou inexistente)");
            }
            if (usuario.Senha != loginViewModelInput.Senha)
            {
                return BadRequest("Houve um erro ao tentar acessar. (Senha incorreta)");
            }

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email

            };
            var token = _authenticationService.GerarToken(usuarioViewModelOutput);
            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permite cadastrar um usuario
        /// </summary>
        /// <param name="registrarViewModelInput">View da Model de Registrar</param>
        /// <returns>Retornar status ok, login e senha ok</returns>
        [SwaggerResponse(statusCode: 201, description: "Criado com Sucesso", Type = typeof(RegistrarViewModelInput), Description = "Criado com Sucesso 2")]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput), Description = "Campos Obrigatorios 2")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel), Description = "Erro Interno 2")]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistrarViewModelInput registrarViewModelInput)
        {
            //var migracoesPendentes = contexto.Database.GetPendingMigrations();
            //if (migracoesPendentes.Count() > 0)
            //{
            //    contexto.Database.Migrate();
            //}

            var usuario = new Usuario()
            {
                Login = registrarViewModelInput.Login,
                Email = registrarViewModelInput.Email,
                Senha = registrarViewModelInput.Senha 
            };
            
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", registrarViewModelInput);
        }
    }
}
