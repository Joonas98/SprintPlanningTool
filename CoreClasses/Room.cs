public class Room
{
	public string RoomId { get; set; } = Guid.NewGuid().ToString();
	public string Name { get; set; } = "";
	public List<Participant> Participants { get; set; } = new List<Participant>();
	public bool PointsRevealed { get; set; } = false;
}
