using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Models;
using curso.api.Models.Curso;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/Curso")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// Cadastramento de Cursos
        /// </summary>
        /// <param name="cursoViewModelInput">View da Model de Curso</param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 201, description: "Curso Criado", Type = typeof(CursoViewModelInput), Description = "Sucesso ao autenticar 2")]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado", Type = typeof(ValidaCampoViewModelOutput), Description = "Campos Obrigatorios 2")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel), Description = "Erro Interno 2")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput) 
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Curso curso = new Curso() 
            {
                Nome = cursoViewModelInput.Nome,
                Descricao = cursoViewModelInput.Descricao,
                CodigoUsuario = codigoUsuario
            };
            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();
            
            return Created("",cursoViewModelInput);
        }

        /// <summary>
        /// Retorna uma Lista dos Cursos Cadastrados
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os cursos", Type = typeof(CursoViewModelInput), Description = "Sucesso ao autenticar 2")]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {            
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario).Select(s => new CursoViewModelOutput() 
            {
                Login = s.Usuario.Login,
                Descricao = s.Descricao,
                Nome = s.Nome                 
            });

            return Ok(cursos);
        }
    }
}
