public class PlanningService
{
	public List<Participant> Participants { get; private set; } = new List<Participant>();
	public bool PointsRevealed { get; private set; } = false;

	public event Action? OnStateChanged;

	public void AddParticipant(string name, bool isScrumMaster = false)
	{
		if (!Participants.Any(p => p.Name == name))
		{
			isScrumMaster = isScrumMaster || !Participants.Any(p => p.IsScrumMaster);

			Participants.Add(new Participant
			{
				Name = name,
				IsScrumMaster = isScrumMaster,
				SelectedPoints = null
			});

			RaiseStateChanged();
		}
	}

	public void UpdateParticipantPoints(string name, string? points)
	{
		var participant = Participants.FirstOrDefault(p => p.Name == name);
		if (participant != null)
		{
			participant.SelectedPoints = points;
			RaiseStateChanged();
		}
	}

	public void RevealPoints(string name)
	{
		if (Participants.Any(p => p.Name == name && p.IsScrumMaster))
		{
			PointsRevealed = true;
			RaiseStateChanged();
		}
	}

	public void Reset(string name)
	{
		if (Participants.Any(p => p.Name == name && p.IsScrumMaster))
		{
			foreach (var participant in Participants)
			{
				participant.SelectedPoints = null;
			}
			PointsRevealed = false;
			RaiseStateChanged();
		}
	}

	private void RaiseStateChanged()
	{
		// Safely invoke the state change event
		OnStateChanged?.Invoke();
	}
}
