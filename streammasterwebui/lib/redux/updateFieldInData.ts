import { FieldData } from '@lib/smAPI/smapiTypes';

export const updateFieldInData = (response: any[] | undefined, fieldData: FieldData): any[] | undefined => {
  if (!response) return undefined;

  return response.map((dto: any) => {
    const id = dto.Id.toString();
    if (id === fieldData.Id) {
      return {
        ...dto,
        [fieldData.Field]: fieldData.Value
      };
    }
    return dto;
  });
};
