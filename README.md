# Sprint Planning Tool

A real-time web application for agile sprint planning, built with **Blazor Server**. This tool enables teams to collaboratively select story points during planning sessions. The Scrum Master oversees the session, reveals story points, and resets rounds as needed.

## Features

- **Real-Time Collaboration**: Teams can select story points and see updates instantly.
- **Scrum Master Role**: One user manages revealing and resetting story points.
- **Responsive Design**: Optimized for desktops and tablets.
- **Local Storage**: Saves user preferences like participant names.

## Tech Stack

- **Frontend**: Blazor Server (ASP.NET Core)
- **Backend**: Singleton services with SignalR for real-time updates
- **Storage**: Browser Local Storage

## How It Works

1. **Enter Your Name**:  
   On the landing page, input your name to join the session.
2. **Select Story Points**:  
   Choose from predefined options like numbers, `?`, or ☕ for trivial tasks.
3. **Scrum Master Controls**:  
   The Scrum Master can reveal or reset story points for all participants.

---

### Notes

- The app supports real-time updates for multiple participants using Blazor Server's built-in SignalR capabilities.
- Local Storage is used to save participant names for convenience across sessions.
