﻿using System;

namespace Finance.Common
{
	public enum AccountRole
	{
		SuperAdmin = 0,
		GroupAdmin = 1,
		User = 2,
		Unknown = -1
	}
	public enum Months
	{
		Jan = 1,
		Feb = 2,
		Mar = 3,
		Apr = 4,
		May = 5,
		Jun = 6,
		Jul = 7,
		Aug = 8,
		Sep = 9,
		Oct = 10,
		Nov = 11,
		Dec = 12
	}
	public enum CardType
	{
		Debit = 1,
		Credit = 2,
		Gift = 3,
		None = -1
	}
}
