import {z} from 'zod'

const requiredString = (fieldName: string) => z.string({error:  `${fieldName} isrequired`})
.min(1, {message: `${fieldName} isrequired`})

export const activitySchema = z.object({
    title : requiredString('title'),
    description : requiredString('description'),
    category : requiredString('category'),
    date : z.coerce.date({
        message : 'Date is required'
    }),
    location: z.object({
        venue : requiredString('Venue'),
        city : z.string().optional(),
        latitude : z.coerce.number(),
        longitude : z.coerce.number()
    })
   

})
export type ActivitySchema = z.input<typeof activitySchema>; 