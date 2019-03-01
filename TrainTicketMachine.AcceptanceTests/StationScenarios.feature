Feature: StationScenarios

Scenario: Partial search
	Given a list of four stations "DARTFORD", "DARTMOUTH", "TOWER HILL", "DERBY"
	When input "DART"
	Then should return characters: 'F', 'M' and stations: "DARTFORD", "DARTMOUTH"

Scenario: Full search
	Given a list of three stations "LIVERPOOL", "LIVERPOOL LIME STREET", "PADDINGTON"
	When input "LIVERPOOL"
	Then should return character: ' ' and stations: "LIVERPOOL", "LIVERPOOL LIME STREET"

Scenario: Unexisting search
	Given a list of three stations "EUSTON", "LONDON BRIDGE", "VICTORIA"
	When input "LIVERPOOL"
	Then should return no stations and no characters