using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using API.Modelos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatosSensorController : ControllerBase
    {
    
        public DatosSensorController(IConfiguration config)
        {
            CadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        public string CadenaSQL { get; }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<DatosSensor> lista = new List<DatosSensor>();
            try
            {
                using (var conexion = new SqlConnection(CadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_DatosSensor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new DatosSensor
                            {
                                ID = Convert.ToInt32(rd["ID"]),
                                Temperatura = Convert.ToSingle(rd["Temperatura"]),
                                Distance1 = Convert.ToSingle(rd["Distance1"]),
                                Distance2 = Convert.ToSingle(rd["Distance2"]),
                                TDSValue = Convert.ToSingle(rd["TDSValue"]),
                                PHValue = Convert.ToSingle(rd["PHValue"]),
                                FechaHora = Convert.ToDateTime(rd["FechaHora"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }
        }
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] DatosSensor objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(CadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_DatosSensor", conexion);
                    cmd.Parameters.AddWithValue("@Temperatura", objeto.Temperatura);
                    cmd.Parameters.AddWithValue("@Distance1", objeto.Distance1);
                    cmd.Parameters.AddWithValue("@Distance2", objeto.Distance2);
                    cmd.Parameters.AddWithValue("@TDSValue", objeto.TDSValue);
                    cmd.Parameters.AddWithValue("@PHValue", objeto.PHValue);
                    cmd.Parameters.AddWithValue("@FechaHora", objeto.FechaHora);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos del sensor agregados correctamente." });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] DatosSensor objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(CadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_DatosSensor", conexion);
                    cmd.Parameters.AddWithValue("@ID", objeto.ID);
                    cmd.Parameters.AddWithValue("@Temperatura", objeto.Temperatura);
                    cmd.Parameters.AddWithValue("@Distance1", objeto.Distance1);
                    cmd.Parameters.AddWithValue("@Distance2", objeto.Distance2);
                    cmd.Parameters.AddWithValue("@TDSValue", objeto.TDSValue);
                    cmd.Parameters.AddWithValue("@PHValue", objeto.PHValue);
                    cmd.Parameters.AddWithValue("@FechaHora", objeto.FechaHora);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        [HttpDelete]
        [Route("Eliminar/{ID:int}")]
        public IActionResult Eliminar(int ID)
        {
            try
            {
                using (var conexion = new SqlConnection(CadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_DatosSensor", conexion);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }




    }
}