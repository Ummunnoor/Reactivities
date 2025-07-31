import { format } from "date-fns";
import z from "zod";

export function formatDate(date: Date){
    return format(date, "dd MMM yyyy h:mm a")
}
export const requiredString = (fieldName: string) => z.string({error:  `${fieldName} isrequired`})
.min(1, {message: `${fieldName} isrequired`})