SELECT
		Id,
		SessionId,
		Nickname
FROM
		[User]
WHERE
		SessionId = @SessionId