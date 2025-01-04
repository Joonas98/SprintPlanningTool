public class PlanningService
{
	private readonly Dictionary<string, Room> _rooms = new();

	public event Action? OnStateChanged;

	/// <summary>
	/// Creates a new room with the specified name.
	/// </summary>
	/// <param name="roomName">The name of the room to create.</param>
	/// <returns>The unique ID of the newly created room.</returns>
	public string CreateRoom(string roomName)
	{
		var room = new Room
		{
			RoomId = Guid.NewGuid().ToString(),
			Name = roomName
		};

		_rooms.Add(room.RoomId, room);
		RaiseStateChanged();
		return room.RoomId;
	}

	/// <summary>
	/// Retrieves a room by its unique ID.
	/// </summary>
	/// <param name="roomId">The unique ID of the room.</param>
	/// <returns>The room object, or null if not found.</returns>
	public Room? GetRoom(string roomId)
	{
		_rooms.TryGetValue(roomId, out var room);
		return room;
	}

	/// <summary>
	/// Retrieves a room by its name.
	/// </summary>
	/// <param name="roomName">The name of the room.</param>
	/// <returns>The room object, or null if not found.</returns>
	public Room? GetRoomByName(string roomName)
	{
		return _rooms.Values.FirstOrDefault(room => string.Equals(room.Name, roomName, StringComparison.OrdinalIgnoreCase));
	}

	/// <summary>
	/// Adds a participant to a room.
	/// </summary>
	/// <param name="roomId">The ID of the room.</param>
	/// <param name="name">The name of the participant.</param>
	/// <param name="isScrumMaster">Whether the participant is a Scrum Master.</param>
	public void AddParticipant(string roomId, string name, bool isScrumMaster = false)
	{
		if (_rooms.TryGetValue(roomId, out var room))
		{
			if (!room.Participants.Any(p => p.Name == name))
			{
				isScrumMaster = isScrumMaster || !room.Participants.Any(p => p.IsScrumMaster);

				room.Participants.Add(new Participant
				{
					Name = name,
					IsScrumMaster = isScrumMaster,
					SelectedPoints = null
				});

				RaiseStateChanged();
			}
		}
	}

	/// <summary>
	/// Updates the selected points for a participant.
	/// </summary>
	/// <param name="roomId">The ID of the room.</param>
	/// <param name="name">The name of the participant.</param>
	/// <param name="points">The points selected by the participant.</param>
	public void UpdateParticipantPoints(string roomId, string name, string? points)
	{
		var room = GetRoom(roomId);
		var participant = room?.Participants.FirstOrDefault(p => p.Name == name);
		if (participant != null)
		{
			participant.SelectedPoints = points;
			RaiseStateChanged();
		}
	}

	/// <summary>
	/// Reveals the points for all participants in a room.
	/// </summary>
	/// <param name="roomId">The ID of the room.</param>
	/// <param name="name">The name of the Scrum Master revealing the points.</param>
	public void RevealPoints(string roomId, string name)
	{
		var room = GetRoom(roomId);
		if (room != null && room.Participants.Any(p => p.Name == name && p.IsScrumMaster))
		{
			room.PointsRevealed = true;
			RaiseStateChanged();
		}
	}

	/// <summary>
	/// Resets the points for all participants in a room.
	/// </summary>
	/// <param name="roomId">The ID of the room.</param>
	/// <param name="name">The name of the Scrum Master resetting the points.</param>
	public void ResetRoom(string roomId, string name)
	{
		var room = GetRoom(roomId);
		if (room != null && room.Participants.Any(p => p.Name == name && p.IsScrumMaster))
		{
			foreach (var participant in room.Participants)
			{
				participant.SelectedPoints = null;
			}
			room.PointsRevealed = false;
			RaiseStateChanged();
		}
	}

	/// <summary>
	/// Invokes the state change event to notify subscribers.
	/// </summary>
	private void RaiseStateChanged()
	{
		OnStateChanged?.Invoke();
	}
}
