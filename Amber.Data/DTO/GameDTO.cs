using System.ComponentModel.DataAnnotations;
namespace Amber.Data.DTO
{
    public class GameRequestDTO
    {
        [Required]
        public string MapName { get; set; }
        [Required]
        public string TankOneName { get; set; }
        [Required]
        public string TankTwoName { get; set; }
        [MinLength(2)]
        [MaxLength(2)]
        public int[] TankOnePos { get; set; }
        [MinLength(2)]
        [MaxLength(2)]
        public int[] TankTwoPos { get; set; }
    }

    public class GameResponseDTO
    {
        public string MapName { get; set; }
        public string TankOneName { get; set; }
        public string TankTwoName { get; set; }
        public string Summary { get; set; }

        public GameResponseDTO(GameRequestDTO request)
        {
            MapName = request.MapName;
            TankOneName = request.TankOneName;
            TankTwoName = request.TankTwoName;
        }
    }
}
