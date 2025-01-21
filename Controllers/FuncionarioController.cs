using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrilhaNetAzureDesafio.Context;
using TrilhaNetAzureDesafio.Models;

namespace TrilhaNetAzureDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly RHContext _context;
        private readonly TableServiceClient _tableServiceClient;
        private readonly string _tableName;
        private readonly ILogger<FuncionarioController> _logger;

        public FuncionarioController(RHContext context, IConfiguration configuration, ILogger<FuncionarioController> logger)
        {
            _context = context;
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SAConnectionString");
            _tableName = configuration.GetValue<string>("ConnectionStrings:AzureTableName");
            _tableServiceClient = new TableServiceClient(_connectionString);
            _logger = logger;
        }

        // Obtém um funcionário por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var funcionario = await _context.Funcionarios.FindAsync(id);
                if (funcionario == null)
                {
                    return NotFound();
                }

                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter funcionário com ID {id}");
                return StatusCode(500, "Ocorreu um erro ao obter o funcionário.");
            }
        }

        // Cria um novo funcionário
        [HttpPost]
        public async Task<IActionResult> Criar(Funcionario funcionario)
        {
            // ... (código já apresentado anteriormente, com melhorias)
        }

        // Atualiza um funcionário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Funcionario funcionario)
        {
            // ... (código já apresentado anteriormente, com melhorias)
        }

        // Exclui um funcionário
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            // ... (código já apresentado anteriormente, com melhorias)
        }

        // Método auxiliar para obter o cliente do Azure Table
        private TableClient GetTableClient()
        {
            var tableClient = _tableServiceClient.GetTableClient(_tableName);
            tableClient.CreateIfNotExists();
            return tableClient;
        }
    }
}