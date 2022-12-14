using APITestProject.Models;
using APITestProject.Parameter;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APITestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        /// <summary>
        /// 測試用的資料集合
        /// </summary>
        private static List<Card> _cards = new List<Card>();

        [HttpGet]
        public List<Card> GetList()
        {
            return _cards;
        }

        /// <summary>
        /// 查單張卡片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Card Get([FromRoute] int id)
        {
            return _cards.FirstOrDefault(card => card.Id == id);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] CardParameter parameter)
        {
            _cards.Add(
                new Card { 
                    Id = _cards.Any() ? _cards.Max(card => card.Id) + 1 : 0,
                    Name = parameter.Name,
                    Description = parameter.Description
                });
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id , [FromBody] CardParameter parameter)
        {
            var targetCard = _cards.FirstOrDefault(card => card.Id == id);
            if(targetCard is null)
            {
                return NotFound();
            }
            targetCard.Name = parameter.Name;
            targetCard.Description = parameter.Description;

            return Ok();
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _cards.RemoveAll(card => card.Id == id);
            return Ok();
        }

    }
}
