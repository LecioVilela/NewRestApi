using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NewRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private bool isNumber;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        /// <summary>Rota para cálculo entre dois números.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar dois números para realizar uma adição.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult GetSum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        /// <summary>Rota para cálculo entre dois números.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar dois números para realizar uma subtração.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("minus/{firstNumber}/{secondNumber}")]
        public IActionResult GetMinus(string firstNumber, string secondNumber)
        {
            // Validação dos parametros.
            if (string.IsNullOrEmpty(firstNumber) || string.IsNullOrEmpty(secondNumber))
            {
                return BadRequest("Ooops! É impossível uma operação com apenas um número ou nenhum!");
            }

            // Processamento dos parametros.
            try
            {
                if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                {
                    var minus = Convert.ToDecimal(firstNumber) - Convert.ToDecimal(secondNumber);
                    return Ok(minus.ToString());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            return BadRequest("Invalid Input");
        }

        /// <summary>Rota para cálculo entre dois números.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar dois números para realizar uma divisão.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult GetDivision(string firstNumber, string secondNumber)
        {
            // Validação dos parametros
            if (string.IsNullOrEmpty(firstNumber) || string.IsNullOrEmpty(secondNumber))
            {
                return BadRequest("Ooops! É impossível uma operação com apenas um número ou nenhum!");
            }

            try
            {
                if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                {
                    if (Convert.ToDecimal(firstNumber) < Convert.ToDecimal(secondNumber))
                    {
                        return BadRequest("Ooops! Em uma divisão o primeiro número não pode ser menor que o segundo...");
                    }

                    var div = Convert.ToDecimal(firstNumber) / Convert.ToDecimal(secondNumber);
                    return Ok(div.ToString());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            return BadRequest("Invalid Input!");
        }

        /// <summary>Rota para cálculo entre dois números.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar dois números para realizar uma multiplicação.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult GetMultiplication(string firstNumber, string secondNumber)
        {
            if (string.IsNullOrEmpty(firstNumber) || string.IsNullOrEmpty(secondNumber))
            {
                return BadRequest("Ooops! É impossível uma operação com apenas um número ou nenhum!");
            }

            try
            {
                if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                {
                    var div = Convert.ToDecimal(firstNumber) * Convert.ToDecimal(secondNumber);
                    return Ok(div.ToString());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            return BadRequest("Invalid Input!");
        }

        /// <summary>Rota para raiz quadrada de um número.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar um número para obter sua raiz quadrada.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("sqrt/{numberToSqrt}")]
        public IActionResult GetSqrt(string numberToSqrt)
        {
            if (string.IsNullOrEmpty(numberToSqrt))
            {
                return BadRequest("Oooops! É necessário informar um número.");
            }

            try
            {
                if (IsNumeric(numberToSqrt))
                {
                    var sqrt = Math.Sqrt(Convert.ToDouble(numberToSqrt));
                    return Ok(sqrt.ToString());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            return BadRequest("Invalid Input!");
        }

        /// <summary>Rota para média entre dois valores.</summary>
        /// <remarks>
        /// ### Fluxo de Utilização da rota: ###
        ///     - Informar dois números para obter sua média.
        ///</remarks>
        /// <returns></returns>
        [HttpGet("medium/{firstNumber}/{secondNumber}")]
        public IActionResult GetMed(string firstNumber, string secondNumber)
        {
            if (string.IsNullOrEmpty(firstNumber) || string.IsNullOrEmpty(secondNumber))
            {
                return BadRequest("Oooops! Não é possível obter a média de um ou nenhum número!");
            }

            try
            {
                if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                {
                    var med = Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber) / 2;
                    return Ok(med.ToString());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
            return BadRequest("Invalid Input!");
        }
        private bool IsNumeric(string strNumber)
        {
            double number;
            isNumber = double.TryParse(strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);
            return isNumber;
        }
    }
}
