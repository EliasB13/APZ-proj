using APZ_BACKEND.Core.Dtos.Readers;
using APZ_BACKEND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_BACKEND.Core.Mappers
{
	public static class ReaderMapper
	{
		public static ReaderDto ToDto(this Reader reader)
		{
			return new ReaderDto()
			{
				ReaderId = reader.Id
			};
		}
	}
}
