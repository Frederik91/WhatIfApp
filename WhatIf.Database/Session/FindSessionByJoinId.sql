SELECT
		Id,
		JoinId,
		[Name],
		LeaderId,
		[Started],
		Ended
FROM
		[Session]
WHERE
		JoinId = @JoinId AND
		[Started] = 0 AND
		Ended = 0