import { format, formatDistanceToNow } from "date-fns";
import z from "zod";

export function formatDate(date: Date){
    return format(date, "dd MMM yyyy h:mm a")
}

export function timeAgo(date : Date){
    return formatDistanceToNow(date) + ' ago'
}

export const requiredString = (fieldName: string) => z.string({required_error:  `${fieldName} isrequired`})
.min(1, {message: `${fieldName} isrequired`})