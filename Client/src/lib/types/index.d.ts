type Activity = {
  id: string;
  title: string;
  description: string;
  category: string;
  date: Date;
  isCancelled: boolean;
  city: string;
  venue: string;
  longitude: number;
  latitude: number;
  attendees: profile[];
  isHost : boolean;
  isGoing: boolean;
  hostId : string
  hostDisplayName : string
};

type Profile = {
  id : string
  displayName : string
  bio? : string
  imageUrl? : string
}

type User = {
  displayName: string;
  id: string;
  email: string;
  imageUrl?: string;
};

type LocationIQSuggestion = {
  place_id: string;
  osm_id: string;
  osm_type: string;
  licence: string;
  lat: string;
  lon: string;
  boundingbox: string[];
  class: string;
  type: string;
  display_name: string;
  display_place: string;
  display_address: string;
  address: LocationIQAddress;
};

type LocationIQAddress = {
  name: string;
  road?: string;
  neighbourhood?: string;
  suburb?: string;
  town?: string;
  village?: string;
  city?: string;
  county: string;
  state: string;
  postcode?: string;
  country: string;
  country_code: string;
};
