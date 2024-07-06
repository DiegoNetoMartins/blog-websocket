using System.Net.WebSockets;

var cts = new CancellationTokenSource();

Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

var tasks = new Task[3];
for (int i = 0; i < 3; i++)
{
    tasks[i] = RunWebSocketClient($"Client {i + 1:00}", cts.Token);
}

await Task.WhenAll(tasks);

static async Task RunWebSocketClient(string name, CancellationToken cancellationToken)
{
    var client = new ClientWebSocket();

    await client.ConnectAsync(new Uri("wss://localhost:7021/notifications"), cancellationToken);

    Console.WriteLine($"{name} connected.");

    var buffer = new byte[1024 * 4];

    while (!cancellationToken.IsCancellationRequested && client.State == WebSocketState.Open)
    {
        var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

        if (result.MessageType == WebSocketMessageType.Text)
        {
            var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"{name} received: {message}");
        }
    }

    await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);

    Console.WriteLine($"{name} disconnected.");
}